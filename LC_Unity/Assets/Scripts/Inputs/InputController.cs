using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

namespace Inputs
{
    public enum InputAction
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Select,
        Cancel,
        OpenMenu
    }

    public class InputController : MonoBehaviour
    {
        #region Fields
        private List<Touch> _touches;
        #endregion

        #region Events
        public UnityEvent<InputAction> ButtonClicked { get; set; }
        public UnityEvent<Vector2> LeftClick { get; set; }
        public UnityEvent<Vector2> RightClick { get; set; }
        public UnityEvent<List<Touch>> TouchesOnScreen { get; set; }
        public UnityEvent NoTouchOnScreen { get; set; }
        public UnityEvent NoMovement { get; set; }
        #endregion

        private void Awake()
        {
            ButtonClicked = new UnityEvent<InputAction>();
            LeftClick = new UnityEvent<Vector2>();
            RightClick = new UnityEvent<Vector2>();
            TouchesOnScreen = new UnityEvent<List<Touch>>();
            NoTouchOnScreen = new UnityEvent();
            NoMovement = new UnityEvent();

            _touches = new List<Touch>();

            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            HandleAxes();
        }

        private void Update()
        {
            HandleButtons();
            HandleClicks();
            HandleTouches();
        }

        private void HandleAxes()
        {
            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                ButtonClicked.Invoke(InputAction.MoveRight);
            }
            else if (Input.GetAxis("Horizontal") < 0.0f)
            {
                ButtonClicked.Invoke(InputAction.MoveLeft);
            }

            if (Input.GetAxis("Vertical") > 0.0f)
            {
                ButtonClicked.Invoke(InputAction.MoveUp);
            }
            else if (Input.GetAxis("Vertical") < 0.0f)
            {
                ButtonClicked.Invoke(InputAction.MoveDown);
            }
        }

        private void HandleButtons()
        {
            if(Input.GetButtonDown("Select"))
            {
                ButtonClicked.Invoke(InputAction.Select);
            }

            if(Input.GetButtonDown("Cancel"))
            {
                ButtonClicked.Invoke(InputAction.Cancel);
            }

            if(Input.GetButtonDown("Submit"))
            {
                ButtonClicked.Invoke(InputAction.OpenMenu);
            }
        }

        private void HandleClicks()
        {
            if(Input.GetButton("Fire1"))
            {
                LeftClick.Invoke(Input.mousePosition);
            }

            if(Input.GetButtonDown("Fire2"))
            {
                RightClick.Invoke(Input.mousePosition);
            }
        }

        private void HandleTouches()
        {
            _touches.Clear();

            if (Input.touchCount > 0)
            {
                for(int i = 0; i < Input.touchCount; i++)
                {
                    _touches.Add(Input.touches[i]);
                }

                TouchesOnScreen.Invoke(_touches);
            }

            if(_touches.Count == 0)
                NoTouchOnScreen.Invoke();
        }

        public void SendDirection(Vector2Int direction)
        {
            if (direction.x == 1)
            {
                ButtonClicked.Invoke(InputAction.MoveRight);
                if (direction.y == 1)
                {
                    ButtonClicked.Invoke(InputAction.MoveUp);
                }
                else if (direction.y == -1)
                {
                    ButtonClicked.Invoke(InputAction.MoveDown);
                }
            }
            else if (direction.x == 0)
            {
                if (direction.y == 1)
                {
                    ButtonClicked.Invoke(InputAction.MoveUp);
                }
                else if (direction.y == -1)
                {
                    ButtonClicked.Invoke(InputAction.MoveDown);
                }
            }
            else if (direction.x == -1)
            {
                ButtonClicked.Invoke(InputAction.MoveLeft);
                if (direction.y == 1)
                {
                    ButtonClicked.Invoke(InputAction.MoveUp);
                }
                else if (direction.y == -1)
                {
                    ButtonClicked.Invoke(InputAction.MoveDown);
                }
            }
            else
            {
                NoMovement.Invoke();
            }
                
        }

        public void SendButtonInput(InputAction action)
        {
            ButtonClicked.Invoke(action);
        }
    }
}