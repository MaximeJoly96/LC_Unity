using UnityEngine;
using Engine.Message;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Dialogs
{
    public class InputNumberBox : MonoBehaviour
    {
        private DisplayInputNumber _inputNumber;
        private int _horizontalCursorIndex;
        private List<SelectableDialogItem> _selectableNumbers;

        [SerializeField]
        private SelectableDialogItem _selectableNumberPrefab;
        [SerializeField]
        private Transform _wrapper;

        public UnityEvent HasClosed { get; set; }
        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void Feed(DisplayInputNumber inputNumber)
        {
            _inputNumber = inputNumber;
            _selectableNumbers = new List<SelectableDialogItem>();

            AdjustWindowSize(_inputNumber.DigitsCount);
        }

        public void FinishedOpening()
        {
            CreateNumbers();

            _horizontalCursorIndex = 0;
            UpdateHorizontalCursorPosition(_horizontalCursorIndex);
        }

        public void Open()
        {
            HasClosed = new UnityEvent();
            Animator.Play("InputNumberOpen");
        }

        public void Close()
        {
            Animator.Play("InputNumberClose");
        }

        public void FinishedClosing()
        {
            _inputNumber.Finished.Invoke();
            HasClosed.Invoke();
        }

        private void CreateNumbers()
        {
            for(int i = 0; i < _inputNumber.DigitsCount; i++)
            {
                SelectableDialogItem number = Instantiate(_selectableNumberPrefab, _wrapper);
                number.SetText(0.ToString());

                _selectableNumbers.Add(number);
            }
        }

        private void UpdateHorizontalCursorPosition(int position)
        {
            for(int i = 0; i < _selectableNumbers.Count; i++)
            {
                _selectableNumbers[i].ShowCursor(i == position);
            }
        }

        public void MoveCursorLeft()
        {
            _horizontalCursorIndex = _horizontalCursorIndex == 0 ? _inputNumber.DigitsCount - 1 : --_horizontalCursorIndex;
            UpdateHorizontalCursorPosition(_horizontalCursorIndex);
        }

        public void MoveCursorRight()
        {
            _horizontalCursorIndex = _horizontalCursorIndex == _inputNumber.DigitsCount - 1 ? 0 : ++_horizontalCursorIndex;
            UpdateHorizontalCursorPosition(_horizontalCursorIndex);
        }

        public void MoveCursorUp()
        {

        }

        public void MoveCursorDown()
        {

        }

        public void Validate()
        {

        }

        private void AdjustWindowSize(int digitsCount)
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(40.0f * digitsCount, rt.sizeDelta.y);
        }
    }
}
