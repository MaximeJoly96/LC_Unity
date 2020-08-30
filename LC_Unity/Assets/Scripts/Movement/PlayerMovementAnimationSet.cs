using System;
using UnityEngine;

namespace LC_Unity.Movement
{
    [Serializable]
    public class PlayerMovementAnimationSet
    {
        public PlayerDirection direction;
        public Sprite[] frames;
    }
}
