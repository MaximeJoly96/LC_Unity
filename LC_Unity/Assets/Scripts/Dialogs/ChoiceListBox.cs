using Engine.Message;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Dialogs
{
    public class ChoiceListBox : MonoBehaviour
    {
        private DisplayChoiceList _choiceList;
        private List<SelectableChoice> _selectableChoices;
        private int _currentSelectionIndex;

        [SerializeField]
        private TMP_Text _message;
        [SerializeField]
        private Transform _choicesWrapper;
        [SerializeField]
        private SelectableChoice _selectableChoicePrefab;

        public UnityEvent HasClosed { get; set; }
        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void Feed(DisplayChoiceList list)
        {
            _choiceList = list;
            _choiceList.Message = _choiceList.Message.Replace("\\n", "<br>");
            _selectableChoices = new List<SelectableChoice>();

            SetPosition();
        }

        public void Open()
        {
            HasClosed = new UnityEvent();
            Animator.Play("ChoiceListOpen");
        }

        public void FinishedOpeningMessage()
        {
            SetMessage();
        }

        public void FinishedOpeningList()
        {
            CreateChoices();

            _currentSelectionIndex = 0;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        private void CreateChoices()
        {
            for (int i = 0; i < _choiceList.Choices.Count; i++)
            {
                SelectableChoice choice = Instantiate(_selectableChoicePrefab, _choicesWrapper);
                choice.SetText(_choiceList.Choices[i].Text);

                _selectableChoices.Add(choice);
            }
        }

        public void Close()
        {
            _message.gameObject.SetActive(false);

            for (int i = 0; i < _selectableChoices.Count; i++)
                _selectableChoices[i].gameObject.SetActive(false);

            Animator.Play("ChoiceListClose");
        }

        public void FinishedClosing()
        {
            _choiceList.Finished.Invoke();
            HasClosed.Invoke();
        }

        public void SetMessage()
        {
            StartCoroutine(AnimateText());
        }

        private IEnumerator AnimateText()
        {
            StringBuilder builder = new StringBuilder();
            WaitForEndOfFrame wait = new WaitForEndOfFrame();

            for (int i = 0; i < _choiceList.Message.Length; i++)
            {
                builder = builder.Append(_choiceList.Message[i]);
                _message.text = builder.ToString();
                yield return wait;
            }
        }

        private void SetPosition()
        {
            RectTransform rt = GetComponent<RectTransform>();

            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x,
                                              124.0f);
        }

        public void MoveCursorUp()
        {
            _currentSelectionIndex = _currentSelectionIndex == 0 ? _selectableChoices.Count - 1 : --_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public void MoveCursorDown()
        {
            _currentSelectionIndex = _currentSelectionIndex == _selectableChoices.Count - 1 ? 0 : ++_currentSelectionIndex;
            UpdateCursorPosition(_currentSelectionIndex);
        }

        public Choice PickChoice()
        {
            return _choiceList.Choices[_currentSelectionIndex];
        }

        private void UpdateCursorPosition(int position)
        {
            for(int i = 0; i < _selectableChoices.Count; i++)
            {
                _selectableChoices[i].ShowCursor(i == position);
            }
        }
    }
}