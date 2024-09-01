using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inputs;

namespace Mobile
{
    public class MobileUiButton : MobileUiElement
    {
        public enum AssociatedButton { A, B, C }

        [SerializeField]
        private AssociatedButton _button;
        [SerializeField]
        private Sprite _pressedSprite;
        [SerializeField]
        private Sprite _notPressedSprite;
        [SerializeField]
        private TMP_Text _text;

        public override bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
                Image img = GetComponent<Image>();

                if(_pressed)
                {
                    img.sprite = _pressedSprite;
                    _text.color = Color.black;
                }
                else
                {
                    img.sprite = _notPressedSprite;
                    _text.color = Color.white;
                }
            }
        }

        protected override void ProcessValidTouches(List<Touch> validTouches)
        {
            base.ProcessValidTouches(validTouches);

            if(validTouches.Count > 0 )
            {
                InputController ctrl = FindObjectOfType<InputController>();

                switch (_button)
                {
                    case AssociatedButton.A:
                        ctrl.SendButtonInput(InputAction.Select);
                        break;
                    case AssociatedButton.B:
                        ctrl.SendButtonInput(InputAction.Cancel);
                        break;
                    case AssociatedButton.C:
                        ctrl.SendButtonInput(InputAction.OpenMenu);
                        break;
                }
            }
        }
    }
}
