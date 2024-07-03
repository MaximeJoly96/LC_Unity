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
        public BaseItem ItemData { get; set; }
        public int InPossession { get; private set; }
        public ItemCategory Category { get; private set; }

        public InventoryItem(BaseItem itemData)
        {
            ItemData = itemData;
            InPossession = 0;
        }

        public void ChangeAmount(int amount)
        {
            if (InPossession + amount < 0)
            {
                LogsHandler.Instance.LogError("Cannot change amount of item " + ItemData.Id + " because it would be lower than 0.");
                return;
            }

            InPossession += amount;
            InPossession = Mathf.Clamp(InPossession, 0, 99);
        }
    }
}
