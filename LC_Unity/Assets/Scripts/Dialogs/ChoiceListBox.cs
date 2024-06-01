using Engine.Message;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;
using UnityEngine.Events;

namespace Dialogs
{
    public class ChoiceListBox : MonoBehaviour
    {
        private DisplayChoiceList _choiceList;

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
            for(int i = 0; i < _choiceList.Choices.Count; i++)
            {
                SelectableChoice choice = Instantiate(_selectableChoicePrefab, _choicesWrapper);
                choice.SetText(_choiceList.Choices[i].Text);
            }
        }

        public void Close()
        {

        }

        public void FinishedClosing()
        {
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
    }
}