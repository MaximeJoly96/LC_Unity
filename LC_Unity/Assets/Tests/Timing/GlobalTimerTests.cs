using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using Timing;
using System.Collections.Generic;

namespace Testing.Timing
{
    public class GlobalTimerTests
    {
        private List<GameObject> _usedGameObjects;

        [UnityTest]
        public IEnumerator TrackGlobalTimerTest()
        {
            GlobalTimer globalTimer = Setup();

            Assert.IsTrue(Mathf.Abs(0.0f - globalTimer.InGameTimeSeconds) < 0.001f);

            globalTimer.Running = true;

            yield return null;

            Assert.IsTrue(globalTimer.InGameTimeSeconds > 0.0f);
        }

        [UnityTest]
        public IEnumerator InitInGameTimerTest()
        {
            GlobalTimer globalTimer = Setup();

            globalTimer.InitInGameTimer(30.0f);

            Assert.IsTrue(Mathf.Abs(30.0f - globalTimer.InGameTimeSeconds) < 0.001f);

            globalTimer.Running = true;

            yield return null;

            Assert.IsTrue(globalTimer.InGameTimeSeconds > 30.0f);
        }

        [UnityTest]
        public IEnumerator ChangeOfSecondIsDetectedTest()
        {
            GlobalTimer globalTimer = Setup();
            globalTimer.Running = true;

            globalTimer.GlobalTimeChangedEvent.AddListener((x) => Assert.IsTrue(globalTimer.InGameTimeSeconds > 1.0f));

            yield return new WaitForSeconds(1.0f);
        }

        private GlobalTimer Setup()
        {
            GameObject timer = new GameObject("Timer");
            _usedGameObjects.Add(timer);
            GlobalTimer globalTimer = timer.AddComponent<GlobalTimer>();

            return globalTimer;
        }

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
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
