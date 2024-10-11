using UnityEngine;
using System.Collections;
using Core;
using Actors;

namespace Menus.SubMenus
{
    public abstract class SubMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform _boundView;

        protected InputReceiver _inputReceiver;
        protected bool _busy;
        protected Character _fedCharacter;

        public abstract void Open();
        public abstract void Close();

        protected virtual void Start()
        {
            _inputReceiver = GetComponent<InputReceiver>();
            BindInputs();
        }

        protected abstract void BindInputs();
        protected abstract bool CanReceiveInput();

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

        protected abstract void FinishedClosing();

        public void Feed(Character character)
        {
            _fedCharacter = character;
        }
    }
}
