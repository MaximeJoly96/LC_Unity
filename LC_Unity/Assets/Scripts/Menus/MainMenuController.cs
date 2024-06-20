using UnityEngine;
using Inputs;
using System.Collections;

namespace Menus
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        protected Canvas _canvas;

        protected InputController _inputController;
        protected bool _busy;

        private void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(HandleInputs);
        }

        private void HandleInputs(InputAction input)
        {
            if(!_busy)
            {
                switch (input)
                {
                    case InputAction.OpenMenu:
                        StartCoroutine(OpenMenu());
                        break;
                    case InputAction.Cancel:
                        StartCoroutine(CloseMenu());
                        break;
                }
            }
        }

        protected IEnumerator OpenMenu()
        {
            _busy = true;
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.interactable = true;
            _busy = false;
        }

        protected IEnumerator CloseMenu()
        {
            _busy = true;
            CanvasGroup group = _canvas.GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for(float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            _busy = false;
        }
    }
}
