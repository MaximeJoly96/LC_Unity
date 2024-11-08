using UnityEngine;
using UnityEngine.Events;
using NUnit.Framework;
using System.Collections.Generic;
using Field;

namespace Testing.Field
{
    public class MapTransitionTests
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

        private MapTransition CreateDefaultTransition()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            MapTransition transition = go.AddComponent<MapTransition>();
            go.AddComponent<BoxCollider2D>();

            return transition;
        }

        [Test]
        public void TransitionCanBeTriggeredManually()
        {
            int value = 0;

            MapTransition transition = CreateDefaultTransition();
            transition.TransitionnedToMap.RemoveAllListeners();
            transition.TransitionnedToMap.AddListener((x) => value++);

            Assert.AreEqual(0, value);

            transition.TriggerTransition();

            Assert.AreEqual(1, value);
        }

        [Test]
        public void TransitionCanBeTriggeredUsingRaycast()
        {
            int value = 0;

            MapTransition transition = CreateDefaultTransition();
            transition.TransitionnedToMap.RemoveAllListeners();
            transition.TransitionnedToMap.AddListener((x) => value++);

            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(2.0f, 0.0f, 0.0f), Vector3.left);

            Assert.AreEqual(1, hits.Length);

            hits[0].collider.GetComponent<MapTransition>().TriggerTransition();

            Assert.AreEqual(1, value);
        }
    }
}
