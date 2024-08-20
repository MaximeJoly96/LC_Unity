using System.Collections.Generic;
using Inventory;

namespace Shop
{
    public class Merchant
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<BaseItem> Items { get; private set; }
        public List<ItemCategory> SoldItemsTypes
        {
            get
            {
                List<ItemCategory> categories = new List<ItemCategory>();

                for(int i = 0; i < Items.Count; i++)
                {
                    if (!categories.Contains(Items[i].Category))
                        categories.Add(Items[i].Category);
                }

                return categories;
            }
        }

        public Merchant(int id, string name)
        {
            Id = id;
            Name = name;

            Items = new List<BaseItem>();
        }

        public void AddItem(BaseItem item)
        {
            Items.Add(item);
        }
    }
}
