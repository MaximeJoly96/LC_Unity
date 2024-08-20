using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemsWrapper : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _weapons;
        [SerializeField]
        private TextAsset _consumables;
        [SerializeField]
        private TextAsset _armours;
        [SerializeField]
        private TextAsset _accessories;
        [SerializeField]
        private TextAsset _resources;
        [SerializeField]
        private TextAsset _keyItems;

        public static ItemsWrapper Instance { get; private set; }

        public List<Consumable> Consumables { get; private set; }
        public List<Weapon> Weapons { get; private set; }
        public List<Armour> Armours { get; private set; }
        public List<Accessory> Accessories { get; private set; }
        public List<Resource> Resources { get; private set; }
        public List<KeyItem> KeyItems { get; private set; }

        private List<BaseItem> _allItems;
        private List<BaseItem> AllItems
        {
            get
            {
                if(_allItems == null)
                {
                    _allItems = new List<BaseItem>();

                    _allItems.AddRange(Consumables);
                    _allItems.AddRange(Weapons);
                    _allItems.AddRange(Armours);
                    _allItems.AddRange(Resources);
                    _allItems.AddRange(Accessories);
                    _allItems.AddRange(KeyItems);
                }

                return _allItems;
            }
        } 

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            FeedWeapons(_weapons);
            FeedItems(_consumables);
            FeedArmours(_armours);
            FeedAccessories(_accessories);
            FeedResources(_resources);
            FeedKeyItems(_keyItems);
        }

        public void FeedWeapons(TextAsset weaponsFile)
        {
            Weapons = WeaponsParser.ParseWeapons(weaponsFile);
        }

        public void FeedItems(TextAsset consumablesFile)
        {
            Consumables = ConsumablesParser.ParseConsumables(consumablesFile);
        }

        public void FeedArmours(TextAsset armoursFile)
        {
            Armours = ArmoursParser.ParseArmours(armoursFile);
        }

        public void FeedAccessories(TextAsset accessoriesFile)
        {
            Accessories = AccessoriesParser.ParseAccessories(accessoriesFile);
        }

        public void FeedResources(TextAsset resourcesFile)
        {
            Resources = ResourcesParser.ParseResources(resourcesFile);
        }

        public void FeedKeyItems(TextAsset keyItemsFile)
        {
            KeyItems = KeyItemsParser.ParseKeyItems(keyItemsFile);
        }

        public BaseItem GetItemFromId(int id)
        {
            return AllItems.FirstOrDefault(i =>  i.Id == id);
        }
    }
}
