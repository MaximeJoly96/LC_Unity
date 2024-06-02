using Inputs;
using Engine.Events;

namespace Dialogs
{
    public class CanvasSelectableController : CanvasMessageController<UiSelectableBox<IRunnable>>
    {
        public string SelectedItem { get; protected set; }

        protected override void ReceiveInput(InputAction input)
        {
            if (_currentItem != null && !_delayOn && string.IsNullOrEmpty(SelectedItem))
            {
                switch (input)
                {
                    case InputAction.MoveDown:
                        _currentItem.MoveCursorDown();
                        break;
                    case InputAction.MoveUp:
                        _currentItem.MoveCursorUp();
                        break;
                    case InputAction.MoveLeft:
                        _currentItem.MoveCursorLeft();
                        break;
                    case InputAction.MoveRight:
                        _currentItem.MoveCursorRight();
                        break;
                    case InputAction.Select:
                        SelectedItem = _currentItem.Validate();
                        _currentItem.Close();
                        break;
                }

                StartSelectionDelay();
            }
        }
    }
}