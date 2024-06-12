﻿using UnityEngine;
using System.Collections;
using Engine.Timing;

namespace Timing
{
    public class Waiter : MonoBehaviour
    {
        public void Wait(Wait wait)
        {
            if (wait.Duration > 0.0f)
                StartCoroutine(DoWait(wait));
            else
                Debug.LogError("You're trying to wait a negative duration. This is not allowed.");
        }

        private IEnumerator DoWait(Wait wait)
        {
            yield return new WaitForSeconds(wait.Duration);

            wait.IsFinished = true;
            wait.Finished.Invoke();
        }
    }
}
