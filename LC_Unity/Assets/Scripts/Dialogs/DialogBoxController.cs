using UnityEngine;
using Inputs;
using Engine.Message;

namespace Dialogs
{
    public class DialogBoxController : CanvasMessageController
    {
        private DialogBox _currentDialogBox;

        [SerializeField]
        private DialogBox _dialogBoxPrefab;

        public void CreateDialog(DisplayDialog dialog)
        {
            _currentDialogBox = Instantiate(_dialogBoxPrefab, _canvas.transform);

            _currentDialogBox.Feed(dialog);
            _currentDialogBox.Open();
            _busy = true;
            _currentDialogBox.HasClosed.AddListener(DestroyCurrentDialog);
            _currentDialogBox.HasFinishedOpening.AddListener(() => _busy = false);
        }

        private void CloseDialog()
        {
            if (_currentDialogBox)
                _currentDialogBox.Close();
        }

        private void DestroyCurrentDialog()
        {
            Destroy(_currentDialogBox.gameObject);
        }

        protected override void BindInputs()
        {
            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                    CloseDialog();
            });
        }

        protected override bool CanReceiveInput()
        {
            return !_busy;
        }
    }
}
