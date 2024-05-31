using UnityEngine.Events;
using UnityEngine;

namespace Inputs
{
    public enum InputAction
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Select,
        Cancel
    }

    public class InputController : MonoBehaviour
    {
        #region Events
        public UnityEvent<InputAction> ButtonClicked { get; set; }
        public UnityEvent<Vector2> LeftClick { get; set; }
        public UnityEvent<Vector2> RightClick { get; set; }
        #endregion

        private void Awake()
        {
            ButtonClicked = new UnityEvent<InputAction>();
            LeftClick = new UnityEvent<Vector2>();
            RightClick = new UnityEvent<Vector2>();
        }

        private void Update()
        {
            HandleAxes();
            HandleButtons();
            HandleClicks();
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
    }
}