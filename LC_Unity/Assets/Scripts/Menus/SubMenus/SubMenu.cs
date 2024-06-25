using UnityEngine;
using System.Collections;
using Inputs;
using Core;
using Actors;

namespace Menus.SubMenus
{
    public abstract class SubMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform _boundView;

        protected bool _busy;
        protected Character _fedCharacter;

        public abstract void Open();
        public abstract void Close();

        protected virtual void Start()
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

            group.alpha = 1.0f;
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

            group.alpha = 0.0f;
            _busy = false;

            yield return new WaitForSeconds(0.2f);
            FinishedClosing();
        }

        protected virtual void HandleInputs(InputAction input)
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

        public void Feed(Character character)
        {
            _fedCharacter = character;
        }
    }
}
