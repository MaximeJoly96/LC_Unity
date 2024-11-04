using UnityEngine;
using System.Collections.Generic;
using Actors;
using Inventory;

namespace Save
{
    public class SavedData
    {
        public Vector2 PlayerPosition { get; set; }
        public int MapID { get; set; }
        public float InGameTimeSeconds { get; set; }
        public List<Character> Party { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public int Gold { get; set; }
    }
}
