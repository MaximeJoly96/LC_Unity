using UnityEngine;
using System.Collections;
using Inputs;
using Core;

namespace Menus.SubMenus
{
    public abstract class SubMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform _boundView;

        protected bool _busy;

        public abstract void Open();
        public abstract void Close();

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(HandleInputs);
        }

        protected IEnumerator DoOpen()
        {
            _busy = true;
            CanvasGroup group = _boundView.GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.interactable = true;
            _busy = false;
        }

        protected IEnumerator DoClose()
        {
            _busy = true;
            CanvasGroup group = _boundView.GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            _busy = false;

            yield return new WaitForSeconds(0.2f);
            FinishedClosing();
        }

        protected void HandleInputs(InputAction input)
        {
            if(!_busy)
            {
                switch (GlobalStateMachine.Instance.CurrentState)
                {
                    case GlobalStateMachine.State.InMenuAbilitiesTab:
                    case GlobalStateMachine.State.InMenuItemsTab:
                    case GlobalStateMachine.State.InMenuEquipmentTab:
                    case GlobalStateMachine.State.InMenuStatusTab:
                    case GlobalStateMachine.State.InMenuSystemTab:
                    case GlobalStateMachine.State.InMenuQuestsTab:
                        if (input == InputAction.Cancel)
                            Close();
                        break;
                }
            }
        }

        protected abstract void FinishedClosing();
    }
}
