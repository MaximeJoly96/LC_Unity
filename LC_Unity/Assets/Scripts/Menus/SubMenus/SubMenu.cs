using UnityEngine;
using System.Collections;

namespace Menus.SubMenus
{
    public abstract class SubMenu : MonoBehaviour
    {
        [SerializeField]
        private Transform _boundView;

        public abstract void Open();
        public abstract void Close();

        protected IEnumerator DoOpen()
        {
            CanvasGroup group = _boundView.GetComponent<CanvasGroup>();
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i < 1.0f; i += 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }

            group.interactable = true;
        }

        protected IEnumerator DoClose()
        {
            CanvasGroup group = _boundView.GetComponent<CanvasGroup>();
            group.interactable = false;
            float currentAlpha = group.alpha;
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            for (float i = currentAlpha; i > 0.0f; i -= 0.05f)
            {
                group.alpha = i;
                yield return wait;
            }
        }
    }
}
