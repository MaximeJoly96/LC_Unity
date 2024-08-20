using UnityEngine;
using System.Collections.Generic;
using Actors;
using Inventory;

namespace Save
{
    public class SavedData
    {
        public Vector2 PlayerPosition { get; internal set; }
        public int MapID { get; internal set; }
        public float InGameTimeSeconds { get; internal set; }
        public List<Character> Party { get; internal set; }
        public List<InventoryItem> Inventory { get; internal set; }
        public int Gold { get; internal set; }
    }
}
