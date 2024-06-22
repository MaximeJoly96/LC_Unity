using UnityEngine;
using Inputs;
using System.Collections;
using Core;

namespace Menus
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        protected Canvas _canvas;

        protected InputController _inputController;

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);
        }

        private void HandleInputs(InputAction input)
        {
            switch (input)
            {
                case InputAction.OpenMenu:
                    if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.OnField)
                        StartCoroutine(OpenMenu());
                    break;
                case InputAction.Cancel:
                    if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenu)
                        StartCoroutine(CloseMenu());
                    break;
            }
        }

        protected IEnumerator OpenMenu()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OpeningMenu);
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.interactable = true;
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        protected IEnumerator CloseMenu()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.ClosingMenu);
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
        }
    }
}
