using UnityEngine;
using Engine.ScreenEffects;
using Logging;
using UnityEngine.UI;
using System.Collections;

namespace ScreenEffects
{
    public class ScreenEffectsHandler : MonoBehaviour
    {
        [SerializeField]
        private Canvas _screenEffectsCanvas;

        private Image CanvasBackground { get { return _screenEffectsCanvas.transform.Find("Background").GetComponent<Image>(); } }

        public void FadeScreen(FadeScreen fade)
        {
            if (_screenEffectsCanvas)
            {
                StartCoroutine(DoFadeScreen(fade));
            }
            else
                LogsHandler.Instance.LogError("You forgot to define screen effects canvas while trying to use FadeScreen.");
        }

        public void FlashScreen(FlashScreen flash)
        {
            if (_screenEffectsCanvas)
            {
                StartCoroutine(DoFlashScreen(flash));
            }
            else
                LogsHandler.Instance.LogError("You forgot to define screen effects canvas while trying to use FlashScreen.");
        }

        public void ShakeScreen(ShakeScreen shake)
        {
            if (_screenEffectsCanvas)
            {
                StartCoroutine(DoShakeScreen(shake));
            }
            else
                LogsHandler.Instance.LogError("You forgot to define screen effects canvas while trying to use ShakeScreen.");
        }

        public void TintScreen(TintScreen tint)
        {
            if (_screenEffectsCanvas)
            {
                StartCoroutine(DoTintScreen(tint));
            }
            else
                LogsHandler.Instance.LogError("You forgot to define screen effects canvas while trying to use TintScreen.");
        }

        private IEnumerator DoFadeScreen(FadeScreen fade)
        {
            float targetAlpha = fade.FadeIn ? 0.0f : 1.0f;
            float alphaChange = fade.FadeIn ? -0.01f : 0.01f;
            Color targetColor = new Color(0.0f, 0.0f, 0.0f, targetAlpha);
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            while(Mathf.Abs(CanvasBackground.color.a - targetColor.a) > 0.01f)
            {
                Color currentColor = CanvasBackground.color;
                currentColor.a += alphaChange;
                CanvasBackground.color = currentColor;
                yield return wait;
            }

            fade.Finished.Invoke();
            fade.IsFinished = true;
        }

        private IEnumerator DoFlashScreen(FlashScreen flash)
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            yield return wait;

            flash.Finished.Invoke();
            flash.IsFinished = true;
        }

        private IEnumerator DoShakeScreen(ShakeScreen shake)
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            yield return wait;

            shake.Finished.Invoke();
            shake.IsFinished = true;
        }

        private IEnumerator DoTintScreen(TintScreen tint)
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();

            yield return wait;

            tint.Finished.Invoke();
            tint.IsFinished = true;
        }
    }
}
