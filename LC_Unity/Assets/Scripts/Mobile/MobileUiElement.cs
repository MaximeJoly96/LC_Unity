using System.Collections.Generic;
using UnityEngine;

namespace Mobile
{
    public class MobileUiElement : MonoBehaviour
    {
        protected bool _pressed;
        protected RectTransform _rt;

        public RectTransform RectT
        {
            get
            {
                if (!_rt)
                    _rt = GetComponent<RectTransform>();

                return _rt;
            }
        }

        public virtual bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
            }
        }

        public virtual void ReceiveTouches(List<Touch> touches)
        {
            List<Touch> validTouches = new List<Touch>();
            for (int i = 0; i < touches.Count; i++)
            {
                Vector2 touchPosition = touches[i].position;
                if (CheckPosition(touchPosition))
                {
                    validTouches.Add(touches[i]);
                }
            }

            Pressed = validTouches.Count > 0;
            ProcessValidTouches(validTouches);
        }

        public virtual void Clear()
        {
            Pressed = false;
        }

        protected virtual void ProcessValidTouches(List<Touch> validTouches) { }

        protected virtual bool CheckPosition(Vector2 position)
        {
            return position.x <= RectT.position.x && position.x >= RectT.position.x - RectT.rect.width &&
                   position.y >= RectT.position.y && position.y <= RectT.position.y + RectT.rect.height;
        }
    }
}
