using System.Collections.Generic;
using Inventory;
using Core.Model;

namespace Shop
{
    public class Merchant
    {
        private ElementIdentifier _identifier;

        public int Id { get { return _identifier.Id; } }
        public string Name { get { return _identifier.NameKey; } }
        public string Description { get { return _identifier.DescriptionKey; } }
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

        public Merchant(ElementIdentifier identifier)
        {
            _identifier = identifier;

            Items = new List<BaseItem>();
        }

        public void AddItem(BaseItem item)
        {
            Items.Add(item);
        }
    }
}
