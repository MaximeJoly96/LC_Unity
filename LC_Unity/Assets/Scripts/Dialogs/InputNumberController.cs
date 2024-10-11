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

        private void DestroyCurrentInputNumber()
        {
            Destroy(_currentInputNumber.gameObject);
        }

        protected override void BindInputs()
        {
            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if(CanReceiveInput())
                    _currentInputNumber.MoveCursorDown();
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                    _currentInputNumber.MoveCursorUp();
            });

            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if (CanReceiveInput())
                    _currentInputNumber.MoveCursorLeft();
            });

            _inputReceiver.OnMoveRight.AddListener(() =>
            {
                if (CanReceiveInput())
                    _currentInputNumber.MoveCursorRight();
            });

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _selectedNumberCode = _currentInputNumber.Validate();
                    _currentInputNumber.Close();
                }
            });
        }

        protected override bool CanReceiveInput()
        {
            return _currentInputNumber != null && string.IsNullOrEmpty(_selectedNumberCode);
        }
    }
}
