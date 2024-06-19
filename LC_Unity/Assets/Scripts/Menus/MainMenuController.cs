using UnityEngine;
using Inputs;
using Logging;

namespace Menus
{
    public class MainMenuController : MonoBehaviour
    {
        protected InputController _inputController;

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);
        }

        private void HandleInputs(InputAction input)
        {
            switch(input)
            {
                case InputAction.OpenMenu:
                    LogsHandler.Instance.Log("open menu");
                    break;
            }
        }
    }
}
