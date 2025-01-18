using UnityEngine;
using System.Collections;

namespace Splash
{
    public abstract class SplashScreenElement : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            StartCoroutine(DoShowElement());
        }

        public virtual void Hide()
        {
            StartCoroutine(DoHideElement());
        }

        protected virtual IEnumerator DoShowElement()
        {
            float alpha = _canvasGroup.alpha;

            while (alpha < 1.0f)
            {
                alpha += 0.02f;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }

        protected virtual IEnumerator DoHideElement()
        {
            float alpha = _canvasGroup.alpha;

            while (alpha >= 0.0f)
            {
                alpha -= 0.02f;
                _canvasGroup.alpha = alpha;

                yield return null;
            }
        }
    }
}
