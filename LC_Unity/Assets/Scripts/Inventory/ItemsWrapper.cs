using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemsWrapper : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _weapons;

        public static ItemsWrapper Instance { get; private set; }

        public List<BaseItem> Items { get; private set; }
        public List<Weapon> Weapons { get; private set; }


        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            FeedWeapons(_weapons);
            FeedItems();
        }

        public void FeedWeapons(TextAsset weaponsFile)
        {
            Weapons = WeaponsParser.ParseWeapons(weaponsFile);
        }

        public void FeedItems()
        {
            Items = new List<BaseItem>();
        }

        public BaseItem GetItemFromId(int id)
        {
            BaseItem item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                item = Weapons.FirstOrDefault(i => i.Id == id);

            return item;
        }
    }
}
