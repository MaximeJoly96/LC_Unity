using UnityEngine;
using Logging;
using System.Text;

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

        public InventoryItem(BaseItem itemData)
        {
            ItemData = itemData;
            InPossession = 0;
        }

        public InventoryItem(int id, int inPossession)
        {
            InPossession = inPossession;
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

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(InPossession);

            return sb.ToString();
        }

        public static InventoryItem Deserialize(int id, string serializedInventoryItem)
        {
            string[] split = serializedInventoryItem.Split(',');

            InventoryItem item = new InventoryItem(id, int.Parse(split[0]));

            return item;
        }
    }
}
