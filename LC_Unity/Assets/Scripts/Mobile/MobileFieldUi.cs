using UnityEngine;
using Inputs;
using System.Collections.Generic;

namespace Mobile
{
    public class MobileFieldUi : MonoBehaviour
    {
        [SerializeField]
        private MobileDPad _dPad;
        [SerializeField]
        private MobileUiButton _buttonA;
        [SerializeField]
        private MobileUiButton _buttonB;
        [SerializeField]
        private MobileUiButton _buttonC;

        private InputController _inputController;

        private void Start()
        {
            if (!Application.isMobilePlatform)
            {
                gameObject.SetActive(false);
            }
            else
            {
                DontDestroyOnLoad(gameObject);

                _inputController = FindObjectOfType<InputController>();
                _inputController.TouchesOnScreen.AddListener(HandleTouches);
                _inputController.NoTouchOnScreen.AddListener(NoTouchesOnScreen);
            }
        }

        private void HandleTouches(List<Touch> touches)
        {
            _dPad.ReceiveTouches(touches);
            _buttonA.ReceiveTouches(touches);
            _buttonB.ReceiveTouches(touches);
            _buttonC.ReceiveTouches(touches);
        }

        private void NoTouchesOnScreen()
        {
            _dPad.Clear();
            _buttonA.Clear();
            _buttonB.Clear();
            _buttonC.Clear();
        }
    }
}
