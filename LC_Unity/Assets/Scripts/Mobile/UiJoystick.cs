using Movement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

namespace Mobile
{
    public class UiJoystick : MobileUiElement
    {
        [SerializeField]
        private RectTransform _cursor;

        public override void ReceiveTouches(List<Touch> touches)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            bool found = false;

            for (int i = 0; i < touches.Count && !found; i++)
            {
                Vector2 touchPosition = touches[i].position;

                if (touchPosition.x <= rectTransform.position.x + rectTransform.rect.width &&
                    touchPosition.x >= rectTransform.position.x &&
                    touchPosition.y >= rectTransform.position.y &&
                    touchPosition.y <= rectTransform.position.y + rectTransform.rect.height)
                {
                    found = true;
                    MapPositionToCursor(touchPosition);
                }
            }
        }

        public override void Clear()
        {
            RectTransform rt = GetComponent<RectTransform>();
            _cursor.transform.position = new Vector2(rt.rect.width / 2.0f,
                                                     rt.rect.height / 2.0f);
        }

        private void MapPositionToCursor(Vector2 position)
        {
            RectTransform rt = GetComponent<RectTransform>();
            _cursor.transform.position = new Vector2(Mathf.Clamp(position.x, rt.position.x, rt.position.x + rt.rect.width),
                                                     Mathf.Clamp(position.y, rt.position.y, rt.position.y + rt.rect.height));

            Vector2 diff = _cursor.transform.position - new Vector3(rt.rect.width / 2.0f, rt.rect.height / 2.0f);
            FindObjectOfType<PlayerController>().MovePlayer(diff.normalized);
        }
    }
}
