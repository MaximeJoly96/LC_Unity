using UnityEngine;
using Logging;

namespace Inventory
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int InPossession { get; private set; }

        public InventoryItem(int id)
        {
            Id = id;
            InPossession = 0;
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
