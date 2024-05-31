using UnityEngine;
using Engine.Message;
using Inputs;

namespace Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        private DialogBox _currentDialogBox;
        private InputController _inputController;

        [SerializeField]
        private DialogBox _dialogBoxPrefab;
        [SerializeField]
        private Canvas _dialogBoxCanvas;

        private void Awake()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(TryToCloseDialog);
            _inputController.LeftClick.AddListener(TryToCloseDialog);
        }

        public void CreateDialog(DisplayDialog dialog)
        {
            _currentDialogBox = Instantiate(_dialogBoxPrefab, _dialogBoxCanvas.transform);

            _currentDialogBox.Feed(dialog);
            _currentDialogBox.Open();
            _currentDialogBox.HasClosed.AddListener(DestroyCurrentDialog);
        }

        private void TryToCloseDialog(Vector2 mousePosition)
        {
            CloseDialog();
        }

        private void TryToCloseDialog(InputAction button)
        {
            if (button == InputAction.Select)
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
