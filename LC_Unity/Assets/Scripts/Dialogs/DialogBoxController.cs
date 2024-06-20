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

        protected override void Start()
        {
            base.Start();
            _inputController.LeftClick.AddListener(TryToCloseDialog);
        }

        public void CreateDialog(DisplayDialog dialog)
        {
            _currentDialogBox = Instantiate(_dialogBoxPrefab, _canvas.transform);

            _currentDialogBox.Feed(dialog);
            _currentDialogBox.Open();
            _busy = true;
            _currentDialogBox.HasClosed.AddListener(DestroyCurrentDialog);
            _currentDialogBox.HasFinishedOpening.AddListener(() => _busy = false);
        }

        protected override void ReceiveInput(InputAction input)
        {
            if(!_busy)
                TryToCloseDialog(input);
        }

        private void TryToCloseDialog(Vector2 mousePosition)
        {
            if(!_busy)
                CloseDialog();
        }

        private void TryToCloseDialog(InputAction button)
        {
            if (button == InputAction.Select && !_busy)
                CloseDialog();
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
    }
}
