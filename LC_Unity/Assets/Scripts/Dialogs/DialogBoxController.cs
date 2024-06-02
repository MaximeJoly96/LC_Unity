using UnityEngine;
using Engine.Events;
using Inputs;

namespace Dialogs
{
    public class DialogBoxController : CanvasMessageController<UiBox<IRunnable>>
    {
        protected override void Start()
        {
            base.Start();
            _inputController.LeftClick.AddListener(TryToCloseDialog);
        }

        protected override void ReceiveInput(InputAction input)
        {
            TryToCloseDialog(input);
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
            if (_currentItem)
                _currentItem.Close();  
        }
    }
}
