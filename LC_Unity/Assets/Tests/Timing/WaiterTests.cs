using NUnit.Framework;
using UnityEngine;
using Timing;
using UnityEngine.TestTools;
using System.Collections;
using Engine.Timing;

namespace Testing.Timing
{
    public class WaiterTests
    {
        [UnityTest]
        public IEnumerator DetectWaitIsFinishedTest()
        {
            GameObject go = new GameObject("Waiter");
            Waiter waiter = go.AddComponent<Waiter>();

            Wait wait = new Wait { Duration = 1.0f };
            wait.Finished.AddListener(() => Assert.IsTrue(wait.IsFinished));

            waiter.Wait(wait);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
