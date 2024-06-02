using UnityEngine;
using Engine.Message;
using Inputs;

namespace Dialogs
{
    public class ChoiceListController : CanvasMessageController
    {
        private ChoiceListBox _currentChoiceList;
        private Choice _selectedChoice;

        [SerializeField]
        private ChoiceListBox _choiceListPrefab;

        public void CreateChoiceList(DisplayChoiceList list)
        {
            _currentChoiceList = Instantiate(_choiceListPrefab, _canvas.transform);

            _currentChoiceList.Feed(list);
            _currentChoiceList.Open();
            _currentChoiceList.HasClosed.AddListener(DestroyCurrentList);
        }

        protected override void ReceiveInput(InputAction input)
        {
            if(_currentChoiceList != null && !_delayOn && _selectedChoice.Id != "")
            {
                if (input == InputAction.Select)
                {
                    _selectedChoice = _currentChoiceList.PickChoice();
                    _currentChoiceList.Close();
                }
                else if (input == InputAction.MoveDown)
                {
                    _currentChoiceList.MoveCursorDown();
                    StartSelectionDelay();
                }
                else if (input == InputAction.MoveUp)
                {
                    _currentChoiceList.MoveCursorUp();
                    StartSelectionDelay();
                }
            }
        }

        private void DestroyCurrentList()
        {
            Destroy(_currentChoiceList.gameObject);
        }
    }
}