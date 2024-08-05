using UnityEngine;
using UnityEngine.Events;

namespace MsgBox
{
    public class MessageBoxService : MonoBehaviour
    {
        public static MessageBoxService Instance { get; private set; }

        [SerializeField]
        private MessageBox _messageBox;

        private UnityEvent<MessageBoxAnswer> _messageBoxClosedWithResult;
        public UnityEvent<MessageBoxAnswer> MessageBoxClosedWithResult
        {
            get
            {
                if (_messageBoxClosedWithResult == null)
                    _messageBoxClosedWithResult = new UnityEvent<MessageBoxAnswer>();

                return _messageBoxClosedWithResult;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            _messageBox.MessageBoxClosedWithResult.AddListener(ReturnResult);
        }

        public void ShowMessage(string message, MessageBoxType messageType)
        {
            _messageBox.Show(message, MessageBoxAnswerType.Ok, messageType);
        }

        public void ShowYesNoMessage(string message, MessageBoxType messageType)
        {
            _messageBox.Show(message, MessageBoxAnswerType.YesNo, messageType);
        }

        public void ShowOkCancelMessage(string message, MessageBoxType messageType)
        {
            _messageBox.Show(message, MessageBoxAnswerType.OkCancel, messageType);
        }

        private void ReturnResult(MessageBoxAnswer result)
        {
            MessageBoxClosedWithResult.Invoke(result);
        }
    }
}
