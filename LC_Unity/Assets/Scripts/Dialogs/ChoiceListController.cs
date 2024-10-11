using UnityEngine;
using Engine.Message;
using Inputs;

namespace Dialogs
{
    public class ChoiceListController : CanvasMessageController
    {
        private ChoiceListBox _currentChoiceList;
        private string _selectedChoice;

        [SerializeField]
        private ChoiceListBox _choiceListPrefab;

        public void CreateChoiceList(DisplayChoiceList list)
        {
            _currentChoiceList = Instantiate(_choiceListPrefab, _canvas.transform);

            _currentChoiceList.Feed(list);
            _currentChoiceList.Open();
            _currentChoiceList.HasClosed.AddListener(DestroyCurrentList);
        }

        private void DestroyCurrentList()
        {
            Destroy(_currentChoiceList.gameObject);
        }

        protected override void BindInputs()
        {
            _inputReceiver.OnSelect.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _selectedChoice = _currentChoiceList.Validate();
                    _currentChoiceList.Close();
                }
            });

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
                    _currentChoiceList.MoveCursorDown();
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                    _currentChoiceList.MoveCursorUp();
            });
        }

        protected override bool CanReceiveInput()
        {
            return _currentChoiceList != null && _selectedChoice != "";
        }
    }
}