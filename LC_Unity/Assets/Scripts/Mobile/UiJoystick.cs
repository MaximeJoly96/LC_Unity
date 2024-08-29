using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mobile
{
    public class UiJoystick : MobileUiElement
    {
        [SerializeField]
        private Transform _cursor;

        public override void ReceiveTouches(List<Touch> touches)
        {
            throw new System.NotImplementedException();
        }

        public override void Clear()
        {
            
        }
    }
}
