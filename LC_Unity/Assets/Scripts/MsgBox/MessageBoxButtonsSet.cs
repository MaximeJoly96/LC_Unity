using UnityEngine;

namespace MsgBox
{
    public class MessageBoxButtonsSet : MonoBehaviour
    {
        [SerializeField]
        private MessageBoxAnswerType _answerType;
        [SerializeField]
        private MessageBoxButton[] _buttons;

        public MessageBoxAnswerType AnswerType { get { return _answerType; } }

        public void UpdateCursor(int cursorPosition)
        {
            for(int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Hover(i == cursorPosition);
            }
        }

        public MessageBoxAnswer Select(int cursorPosition)
        {
            return _buttons[cursorPosition].Select();
        }
    }
}
