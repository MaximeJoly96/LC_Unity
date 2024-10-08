using NUnit.Framework;
using UnityEngine;
using Essence;
using System.Collections.Generic;
using UnityEditor;

namespace Testing.Essence
{
    public class EssencesWrapperTests
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

        [Test]
        public void EssentialAffinitiesCanBeProvidedToWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            
            EssencesWrapper wrapper = go.AddComponent<EssencesWrapper>();
            wrapper.FeedAffinities(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Essence/Essences.xml"));

            Assert.AreEqual(16, wrapper.EssentialAffinities.Count);
        }
    }
}
