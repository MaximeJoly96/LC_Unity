using System.Collections;
using System.Collections.Generic;
using Engine.Events;
using GameProgression;
using NUnit.Framework;
using Timing;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.Engine.Events
{
    public class EventsRunnerTests
    {
        private List<GameObject> _usedGameObjects;

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

        [UnityTest]
        public IEnumerator RunBasicSequenceWithControlVariableAndWaitTest()
        {
            GameObject waiterGo = new GameObject("Waiter");
            _usedGameObjects.Add(waiterGo);
            waiterGo.AddComponent<Waiter>();

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Events/EventsRunnerTestSequence.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);
            bool finished = false;

            sequence.Finished.AddListener(() => finished = true);
            sequence.Events[0].Finished.AddListener(() => Assert.AreEqual(5, PersistentDataHolder.Instance.GetData("var1")));
            sequence.Events[1].Finished.AddListener(() => Assert.AreEqual(5, PersistentDataHolder.Instance.GetData("var1")));
            sequence.Events[2].Finished.AddListener(() => Assert.AreEqual(15, PersistentDataHolder.Instance.GetData("var1")));

            EventsRunner runner = Setup();
            runner.RunEvents(sequence);

            yield return new WaitUntil(() => finished);
        }

        private EventsRunner Setup()
        {
            GameObject go = new GameObject("Runner");
            _usedGameObjects.Add(go);
            return go.AddComponent<EventsRunner>();
        }
    }
}
