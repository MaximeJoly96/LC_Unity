using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Save
{
    public class SaveScreenFader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private UnityEvent _finishedOperation;

        public UnityEvent FinishedOperation
        {
            get
            {
                if (_finishedOperation == null)
                    _finishedOperation = new UnityEvent();

                return _finishedOperation;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Fade(bool fadeIn)
        {
            if(!_canvasGroup)
                _canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(fadeIn ? FadeIn() :  FadeOut());
        }

        private IEnumerator FadeIn()
        {
            float alpha = _canvasGroup.alpha;

            while(alpha > 0.0f)
            {
                alpha -= 0.05f;
                _canvasGroup.alpha = alpha;

                yield return new WaitForSeconds(0.05f);
            }

            _canvasGroup.alpha = 0.0f;
            FinishedOperation.Invoke();
        }

        private IEnumerator FadeOut()
        {
            float alpha = _canvasGroup.alpha;

            while (alpha < 1.0f)
            {
                alpha += 0.05f;
                _canvasGroup.alpha = alpha;

                yield return new WaitForSeconds(0.05f);
            }

            _canvasGroup.alpha = 1.0f;
            FinishedOperation.Invoke();
        }

        public void Reset()
        {
            _canvasGroup.alpha = 0.0f;
        }
    }
}
