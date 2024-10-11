using Inputs;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class InputReceiver : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds

        private float _selectionDelay;
        private bool _delayOn;
        private UnityEvent _onMoveDown;
        private UnityEvent _onMoveUp;
        private UnityEvent _onMoveLeft;
        private UnityEvent _onMoveRight;
        private UnityEvent _onSelect;
        private UnityEvent _onCancel;
        private UnityEvent _onOpenMenu;

        public UnityEvent OnMoveDown
        {
            get
            {
                if(_onMoveDown == null)
                    _onMoveDown = new UnityEvent();

                return _onMoveDown;
            }
        }
        public UnityEvent OnMoveUp
        {
            get
            {
                if (_onMoveUp == null)
                    _onMoveUp = new UnityEvent();

                return _onMoveUp;
            }
        }

        public UnityEvent OnMoveLeft
        {
            get
            {
                if (_onMoveLeft == null)
                    _onMoveLeft = new UnityEvent();

                return _onMoveLeft;
            }
        }

        public UnityEvent OnMoveRight
        {
            get
            {
                if (_onMoveRight == null)
                    _onMoveRight = new UnityEvent();

                return _onMoveRight;
            }
        }

        public UnityEvent OnSelect
        {
            get
            {
                if (_onSelect == null)
                    _onSelect = new UnityEvent();

                return _onSelect;
            }
        }

        public UnityEvent OnCancel
        {
            get
            {
                if (_onCancel == null)
                    _onCancel = new UnityEvent();

                return _onCancel;
            }
        }

        public UnityEvent OnOpenMenu
        {
            get
            {
                if (_onOpenMenu == null)
                    _onOpenMenu = new UnityEvent();

                return _onOpenMenu;
            }
        }

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(HandleInputs);

            _selectionDelay = 0.0f;
            _delayOn = false;
        }

        private void Update()
        {
            if (_delayOn)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }

        private void HandleInputs(InputAction input)
        {
            if(!_delayOn)
            {
                switch(input)
                {
                    case InputAction.MoveDown:
                        OnMoveDown.Invoke();
                        break;
                    case InputAction.MoveUp:
                        OnMoveUp.Invoke();
                        break;
                    case InputAction.MoveLeft:
                        OnMoveLeft.Invoke();
                        break;
                    case InputAction.MoveRight:
                        OnMoveRight.Invoke();
                        break;
                    case InputAction.Select:
                        OnSelect.Invoke();
                        break;
                    case InputAction.Cancel:
                        OnCancel.Invoke();
                        break;
                    case InputAction.OpenMenu:
                        OnOpenMenu.Invoke();
                        break;
                }

                _delayOn = true;
            }
        }
    }
}
