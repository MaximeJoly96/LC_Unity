using Inventory;
using System.Collections.Generic;

namespace Questing
{
    public class QuestReward
    {
        public int Exp { get; private set; }
        public int Gold { get; private set; }
        public List<InventoryItem> Items { get; private set; }

        public QuestReward(int exp, int gold, List<InventoryItem> items)
        {
            Exp = exp;
            Gold = gold;
            Items = items;
        }
    }
}
