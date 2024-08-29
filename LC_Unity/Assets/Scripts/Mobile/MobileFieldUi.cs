using UnityEngine;
using Inputs;
using System.Collections.Generic;

namespace Mobile
{
    public class MobileFieldUi : MonoBehaviour
    {
        [SerializeField]
        private UiJoystick _joystick;
        [SerializeField]
        private MobileUiButton _actionButton;
        [SerializeField]
        private MobileUiButton _menuButton;

        private void Awake()
        {
            if (!Application.isMobilePlatform)
            {
                gameObject.SetActive(false);
            }
            else
            {
                InputController inputController = FindObjectOfType<InputController>();
                inputController.TouchesOnScreen.AddListener(HandleTouches);
                inputController.NoTouchOnScreen.AddListener(NoTouchesOnScreen);
            }
        }

        private void HandleTouches(List<Touch> touches)
        {
            _joystick.ReceiveTouches(touches);
            _actionButton.ReceiveTouches(touches);
            _menuButton.ReceiveTouches(touches);
        }

        private void NoTouchesOnScreen()
        {
            _joystick.Clear();
        }
    }
}
