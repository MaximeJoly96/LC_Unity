using NUnit.Framework;
using Actors;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Essence;

namespace Testing.Actors
{
    public class XmlCharacterParserTests
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
        public void ListOfCharactersCanBeParsed()
        {
            CreateEssencesWrapper();

            List<Character> characters = XmlCharacterParser.ParseCharacters(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestCharacters.xml"));

            Assert.AreEqual(7, characters.Count);
            Assert.AreEqual("Louga", characters[0].Name);
            Assert.AreEqual(48, characters[1].Stats.MaxMana);
            Assert.AreEqual(13, characters[2].EssenceAffinity.Id);
            Assert.AreEqual(11, characters[3].Stats.BaseDefense);
            Assert.AreEqual(6, characters[4].Stats.BaseLuck);
            Assert.AreEqual(326, characters[5].Stats.MaxHealth);
            Assert.AreEqual(2, characters[6].Stats.BaseMagicDefense);
        }

        private EssencesWrapper CreateEssencesWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            EssencesWrapper wrapper = go.AddComponent<EssencesWrapper>();
            wrapper.FeedAffinities(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/Essences.xml"));

            return wrapper;
        }
    }
}
