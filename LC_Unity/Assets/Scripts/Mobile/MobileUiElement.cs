using UnityEngine;
using System.Collections.Generic;

namespace Mobile
{
    public abstract class MobileUiElement : MonoBehaviour
    {
        public abstract void ReceiveTouches(List<Touch> touches);

        public abstract void Clear();
    }
}
