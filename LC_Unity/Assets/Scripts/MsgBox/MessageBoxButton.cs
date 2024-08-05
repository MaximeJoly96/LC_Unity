using UnityEngine;

namespace MsgBox
{
    public class MessageBoxButton : MonoBehaviour
    {
        [SerializeField]
        private Transform _cursor;
        [SerializeField]
        private MessageBoxAnswer _answer;

        public void Hover(bool hover)
        {
            _cursor.gameObject.SetActive(hover);
        }

        public MessageBoxAnswer Select()
        {
            return _answer;
        }
    }
}
