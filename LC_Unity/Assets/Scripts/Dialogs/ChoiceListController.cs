using UnityEngine;
using Engine.Message;
using Inputs;

namespace Dialogs
{
    public class ChoiceListController : CanvasMessageController
    {
        private const float SELECTION_DELAY = 0.2f; // seconds

        private ChoiceListBox _currentChoiceList;
        private Choice _selectedChoice;
        private float _selectionDelayCount;
        private bool _delayOn;

        [SerializeField]
        private ChoiceListBox _choiceListPrefab;

        protected override void Start()
        {
            base.Start();
            _inputController.ButtonClicked.AddListener(UpdateSelectedChoice);

            _selectionDelayCount = 0.0f;
        }

        public void CreateChoiceList(DisplayChoiceList list)
        {
            _currentChoiceList = Instantiate(_choiceListPrefab, _canvas.transform);

            _currentChoiceList.Feed(list);
            _currentChoiceList.Open();
            _currentChoiceList.HasClosed.AddListener(DestroyCurrentList);
        }

        private void UpdateSelectedChoice(InputAction input)
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

        private void Update()
        {
            if(_delayOn)
            {
                _selectionDelayCount += Time.deltaTime;
                if (_selectionDelayCount > SELECTION_DELAY)
                {
                    _selectionDelayCount = 0.0f;
                    _delayOn = false;
                } 
            }
        }

        private void StartSelectionDelay()
        {
            _delayOn = true;
        }

        private void DestroyCurrentList()
        {
            Destroy(_currentChoiceList.gameObject);
        }
    }
}