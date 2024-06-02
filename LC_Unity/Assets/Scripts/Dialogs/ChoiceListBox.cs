using Engine.Message;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace Dialogs
{
    public class ChoiceListBox : UiSelectableBox<DisplayChoiceList>
    {
        [SerializeField]
        private TMP_Text _message;

        protected override string OpenAnimatioName { get { return "ChoiceListOpen"; } }
        protected override string CloseAnimationName { get { return "ChoiceListClose"; } }

        public override void Feed(DisplayChoiceList element)
        {
            base.Feed(element);
            _element.Message = _element.Message.Replace("\\n", "<br>");

            SetPosition();
        }

        public void FinishedOpeningMessage()
        {
            SetMessage();
        }

        protected override void CreateItems()
        {
            for (int i = 0; i < _element.Choices.Count; i++)
            {
                SelectableDialogItem choice = Instantiate(_selectableItemPrefab, _wrapper);
                choice.SetText(_element.Choices[i].Text);

                _selectableItems.Add(choice);
            }
        }

        public override void Close()
        {
            _message.gameObject.SetActive(false);
            base.Close();
        }

        public void SetMessage()
        {
            StartCoroutine(AnimateText(_message, _element.Message));
        }

        private void SetPosition()
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x,
                                              124.0f);
        }

        public override void MoveCursorUp()
        {
            _currentSelectionIndex = _currentSelectionIndex == 0 ? _selectableItems.Count - 1 : --_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public override void MoveCursorDown()
        {
            _currentSelectionIndex = _currentSelectionIndex == _selectableItems.Count - 1 ? 0 : ++_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public override string Validate()
        {
            return _element.Choices[_currentSelectionIndex].Id;
        }
    }
}