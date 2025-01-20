using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Notifications
{
    public abstract class Notification : MonoBehaviour
    {
        protected const float DISPLAY_DURATION = 5.0f;

        protected bool _preparingToHide;
        protected float _hideTimer;

        public void FinishedHiding()
        {
            Destroy(gameObject);
        }

        public void FinishedShowing()
        {
            _preparingToHide = true;
            _hideTimer = 0.0f;
        }

        protected virtual void Update()
        {
            if (_preparingToHide)
            {
                if(_hideTimer >= DISPLAY_DURATION)
                {
                    _preparingToHide = false;
                    Hide();
                    return;
                }

                _hideTimer += Time.deltaTime;
            }
        }

        public virtual void Show()
        {
            GetComponent<Animator>().Play("Show");
        }

        public virtual void Hide()
        {
            GetComponent<Animator>().Play("Hide");
        }
    }
}
