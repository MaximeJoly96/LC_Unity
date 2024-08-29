using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mobile
{
    public class MobileUiButton : MobileUiElement
    {
        public override void ReceiveTouches(List<Touch> touches)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            bool found = false;

            for(int i = 0; i < touches.Count && !found; i++)
            {
                Vector2 touchPosition = touches[i].position;

                if (touchPosition.x >= rectTransform.position.x - rectTransform.rect.width &&
                    touchPosition.x <= rectTransform.position.x &&
                    touchPosition.y >= rectTransform.position.y &&
                    touchPosition.y <= rectTransform.position.y + rectTransform.rect.height)
                {
                    found = true;
                    Execute();
                }
            }
        }

        public override void Clear()
        {
            
        }

        public virtual void Execute() { }
    }
}
