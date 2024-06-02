using UnityEngine;
using Engine.Message;
using Inputs;

namespace Dialogs
{
    public class InputNumberController : CanvasMessageController
    {
        private InputNumberBox _currentInputNumber;
        private string _selectedNumberCode;

        [SerializeField]
        private InputNumberBox _inputNumberPrefab;

        public void CreateInputNumber(DisplayInputNumber inputNumber)
        {
            _currentInputNumber = Instantiate(_inputNumberPrefab, _canvas.transform);

            _currentInputNumber.Feed(inputNumber);
            _currentInputNumber.Open();
            _currentInputNumber.HasClosed.AddListener(DestroyCurrentInputNumber);
        }

        protected override void ReceiveInput(InputAction input)
        {
            if(_currentInputNumber != null && !_delayOn && string.IsNullOrEmpty(_selectedNumberCode))
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        _currentInputNumber.MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        _currentInputNumber.MoveCursorUp();
                        break;
                    case InputAction.MoveLeft:
                        _currentInputNumber.MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        _currentInputNumber.MoveCursorRight();
                        break;
                    case InputAction.Select:
                        _selectedNumberCode = _currentInputNumber.Validate();
                        _currentInputNumber.Close();
                        break;
                }

                StartSelectionDelay();
            }
        }

        private void DestroyCurrentInputNumber()
        {
            Destroy(_currentInputNumber.gameObject);
        }
    }
}
