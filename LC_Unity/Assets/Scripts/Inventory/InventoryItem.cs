using UnityEngine;
using Logging;

namespace Inventory
{
    public enum ItemCategory
    {
        Consumable,
        Resource,
        Weapon,
        Armour,
        Accessory,
        KeyItem
    }

    public class InventoryItem
    {
        public int Id { get; set; }
        public int InPossession { get; private set; }
        public ItemCategory Category { get; private set; }
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public string Description { get; private set; }
        public InventoryItem(int id)
        {
            Id = id;
            InPossession = 0;
        }

        public InventoryItem(int id, string name, ItemCategory category, Sprite icon, string description) : this(id)
        {
            Name = name;
            Category = category;
            Icon = icon;
            Description = description;
        }

        public void ChangeAmount(int amount)
        {
            if (InPossession + amount < 0)
            {
                LogsHandler.Instance.LogError("Cannot change amount of item " + Id + " because it would be lower than 0.");
                return;
            }

            InPossession += amount;
            InPossession = Mathf.Clamp(InPossession, 0, 99);
        }
    }
}
