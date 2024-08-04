using UnityEngine;

namespace Save
{
    public class SavedData
    {
        public Vector2 PlayerPosition { get; internal set; }
        public int MapID { get; internal set; }
        public float InGameTimeSeconds { get; internal set; }
    }
}
