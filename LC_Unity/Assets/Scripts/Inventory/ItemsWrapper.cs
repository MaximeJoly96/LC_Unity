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

                    if(Consumables != null)
                        _allItems.AddRange(Consumables);

                    if(Weapons != null)
                        _allItems.AddRange(Weapons);

                    if(Armours != null)
                        _allItems.AddRange(Armours);

                    if(Resources != null)
                        _allItems.AddRange(Resources);

                    if(Accessories != null)
                        _allItems.AddRange(Accessories);

                    if(KeyItems != null)
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
            FeedConsumables(_consumables);
            FeedArmours(_armours);
            FeedAccessories(_accessories);
            FeedResources(_resources);
            FeedKeyItems(_keyItems);
        }

        public void FeedWeapons(TextAsset weaponsFile)
        {
            if (!weaponsFile)
                return;

            Weapons = WeaponsParser.ParseWeapons(weaponsFile);
        }

        public void FeedConsumables(TextAsset consumablesFile)
        {
            if (!consumablesFile)
                return;

            Consumables = ConsumablesParser.ParseConsumables(consumablesFile);
        }

        public void FeedArmours(TextAsset armoursFile)
        {
            if (!armoursFile)
                return;

            Armours = ArmoursParser.ParseArmours(armoursFile);
        }

        public void FeedAccessories(TextAsset accessoriesFile)
        {
            if (!accessoriesFile)
                return;

            Accessories = AccessoriesParser.ParseAccessories(accessoriesFile);
        }

        public void FeedResources(TextAsset resourcesFile)
        {
            if (!resourcesFile)
                return;

            Resources = ResourcesParser.ParseResources(resourcesFile);
        }

        public void FeedKeyItems(TextAsset keyItemsFile)
        {
            if (!keyItemsFile)
                return;

            KeyItems = KeyItemsParser.ParseKeyItems(keyItemsFile);
        }

        public BaseItem GetItemFromId(int id)
        {
            return AllItems.FirstOrDefault(i =>  i.Id == id);
        }
    }
}
