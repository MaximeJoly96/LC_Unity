using Inputs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mobile
{
    public class MobileDPad : MobileUiElement
    {
        [SerializeField]
        private Sprite _clearDPad;
        [SerializeField]
        private Sprite _leftDPad;
        [SerializeField]
        private Sprite _leftUpDPad;
        [SerializeField]
        private Sprite _upDPad;
        [SerializeField]
        private Sprite _rightUpDPad;
        [SerializeField]
        private Sprite _rightDPad;
        [SerializeField]
        private Sprite _rightDownDPad;
        [SerializeField]
        private Sprite _downDPad;
        [SerializeField]
        private Sprite _leftDownDPad;

        public override bool Pressed 
        { 
            get => base.Pressed; 
            set 
            { 
                base.Pressed = value; 

                if(!Pressed)
                    GetComponent<Image>().sprite = _clearDPad;
            }
        }

        public Vector2 CurrentDirection { get; protected set; }

        protected override void ProcessValidTouches(List<Touch> validTouches)
        {
            base.ProcessValidTouches(validTouches);

            Vector2Int direction = Vector2Int.zero;

            if (validTouches.Count > 0 )
            {
                Touch lastTouch = validTouches[validTouches.Count - 1];
                Vector2 center = new Vector2(RectT.position.x + RectT.rect.width / 2.0f, RectT.position.y + RectT.rect.height / 2.0f);

                Vector2 relativeFromCenter = (lastTouch.position - center).normalized;
                float angle = Vector2.SignedAngle(Vector2.right, relativeFromCenter);
                

                Image img = GetComponent<Image>();

                if (angle <= 30.0f && angle > -30.0f)
                {
                    img.sprite = _rightDPad;
                    direction = new Vector2Int(1, 0);
                }
                else if (angle > 30.0f && angle <= 60.0f)
                {
                    img.sprite = _rightUpDPad;
                    direction = new Vector2Int(1, 1);
                }
                else if (angle > 60.0f && angle <= 120.0f)
                {
                    img.sprite = _upDPad;
                    direction = new Vector2Int(0, 1);
                }
                else if (angle > 120.0f && angle <= 150.0f)
                {
                    img.sprite = _leftUpDPad;
                    direction = new Vector2Int(-1, 1);
                }
                else if (angle > -150.0f && angle <= -120.0f)
                {
                    img.sprite = _leftDownDPad;
                    direction = new Vector2Int(-1, -1);
                }
                else if (angle > -120.0f && angle <= -60.0f)
                {
                    img.sprite = _downDPad;
                    direction = new Vector2Int(0, -1);
                }
                else if (angle > -60.0f && angle <= -30.0f)
                {
                    img.sprite = _rightDownDPad;
                    direction = new Vector2Int(1, -1);
                }
                else
                {
                    img.sprite = _leftDPad;
                    direction = new Vector2Int(-1, 0);
                }
            }

            CurrentDirection = direction;

            FindObjectOfType<InputController>().SendDirection(direction);
        }

        protected override bool CheckPosition(Vector2 position)
        {
            return position.x >= RectT.position.x && position.x <= RectT.position.x + RectT.rect.width &&
                   position.y >= RectT.position.y && position.y <= RectT.position.y + RectT.rect.height;
        }

        public override void Clear()
        {
            base.Clear();
            FindObjectOfType<InputController>().NoMovement.Invoke();
        }
    }
}
