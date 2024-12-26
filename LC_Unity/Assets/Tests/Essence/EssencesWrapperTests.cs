using NUnit.Framework;
using UnityEngine;
using Essence;
using System.Collections.Generic;
using UnityEditor;

namespace Testing.Essence
{
    public class EssencesWrapperTests : TestFoundation
    {
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
