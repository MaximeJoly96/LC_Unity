using UnityEngine;
using Engine.Message;
using System.Collections.Generic;
using System.Text;

namespace Dialogs
{
    public class InputNumberBox : UiSelectableBox<DisplayInputNumber>
    {
        protected override string OpenAnimatioName { get { return "InputNumberOpen"; } }
        protected override string CloseAnimationName { get { return "InputNumberClose"; } }

        public override void Feed(DisplayInputNumber element)
        {
            base.Feed(element);

            AdjustWindowSize(_element.DigitsCount);
        }

        protected override void CreateItems()
        {
            for(int i = 0; i < _element.DigitsCount; i++)
            {
                SelectableDialogItem number = Instantiate(_selectableItemPrefab, _wrapper);
                number.SetText(0.ToString());

                _selectableItems.Add(number);
            }
        }

        public override void MoveCursorLeft()
        {
            _currentSelectionIndex = _currentSelectionIndex == 0 ? _element.DigitsCount - 1 : --_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public override void MoveCursorRight()
        {
            _currentSelectionIndex = _currentSelectionIndex == _element.DigitsCount - 1 ? 0 : ++_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public override void MoveCursorUp()
        {
            int currentValue = int.Parse(_selectableItems[_currentSelectionIndex].TextValue);
            int targetValue = currentValue == 9 ? 0 : ++currentValue;

            _selectableItems[_currentSelectionIndex].SetText(targetValue.ToString());
        }

        public override void MoveCursorDown()
        {
            int currentValue = int.Parse(_selectableItems[_currentSelectionIndex].TextValue);
            int targetValue = currentValue == 0 ? 9 : --currentValue;

            _selectableItems[_currentSelectionIndex].SetText(targetValue.ToString());
        }

        public override string Validate()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _selectableItems.Count; i++)
                sb = sb.Append(_selectableItems[i].TextValue);

            return sb.ToString();
        }

        private void AdjustWindowSize(int digitsCount)
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(40.0f * digitsCount, rt.sizeDelta.y);
        }
    }
}
