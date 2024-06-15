using UnityEngine;
using System.Collections;
using Engine.Timing;
using Logging;

namespace Timing
{
    public class Waiter : MonoBehaviour
    {
        public void Wait(Wait wait)
        {
            if (wait.Duration > 0.0f)
                StartCoroutine(DoWait(wait));
            else
                LogsHandler.Instance.LogError("You're trying to wait a negative duration. This is not allowed.");
        }

        private IEnumerator DoWait(Wait wait)
        {
            yield return new WaitForSeconds(wait.Duration);

            wait.IsFinished = true;
            wait.Finished.Invoke();
        }
    }
}
