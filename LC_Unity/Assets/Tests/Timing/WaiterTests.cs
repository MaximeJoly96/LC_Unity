using NUnit.Framework;
using UnityEngine;
using Timing;
using UnityEngine.TestTools;
using System.Collections;
using Engine.Timing;
using System.Collections.Generic;

namespace Testing.Timing
{
    public class WaiterTests
    {
        private List<GameObject> _usedGameObjects;

        [UnityTest]
        public IEnumerator DetectWaitIsFinishedTest()
        {
            GameObject go = new GameObject("Waiter");
            _usedGameObjects.Add(go);
            Waiter waiter = go.AddComponent<Waiter>();

            Wait wait = new Wait { Duration = 1.0f };
            wait.Finished.AddListener(() => Assert.IsTrue(wait.IsFinished));

            waiter.Wait(wait);

            yield return new WaitForSeconds(1.0f);
        }

        [TearDown]
        public void TearDown()
        {
            for(int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }
    }
}
