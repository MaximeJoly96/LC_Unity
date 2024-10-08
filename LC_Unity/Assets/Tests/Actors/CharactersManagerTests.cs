using NUnit.Framework;
using Actors;
using Core.Model;
using Utils;
using UnityEditor;
using UnityEngine;
using Essence;
using System.Collections.Generic;

namespace Testing.Actors
{
    public class CharactersManagerTests
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

        [SetUp]
        public void Setup()
        {
            CharactersManager.Instance.Characters.Clear();
        }

        [Test]
        public void CharactersManagerCanBeCreated()
        {
            CharactersManager manager = CharactersManager.Instance;

            Assert.AreEqual(0, manager.Characters.Count);
        }

        [Test]
        public void NewCharacterCanBeAdded()
        {
            CreateEssencesWrapper();

            CharactersManager manager = CharactersManager.Instance;
            manager.AddCharacter(new Character(new ElementIdentifier(0, "Louga", ""),
                                               new QuadraticFunction(450, 250, 10),
                                               new StatScalingFunction(22, 1.2f, 256),
                                               new StatScalingFunction(6, 0.8f, 25),
                                               new StatScalingFunction(0, 1.0f, 100),
                                               new StatScalingFunction(1.5f, 1.2f, 8),
                                               new StatScalingFunction(1.4f, 1.1f, 7),
                                               new StatScalingFunction(0.9f, 1.2f, 3),
                                               new StatScalingFunction(1.2f, 1.2f, 8),
                                               new StatScalingFunction(1.7f, 0.975f, 5),
                                               new StatScalingFunction(4, 0.85f, 6)));

            Assert.AreEqual(1, manager.Characters.Count);
            Assert.AreEqual("Louga", manager.Characters[0].Name);
        }

        [Test]
        public void CharactersCanBeLoadedFromFile()
        {
            CreateEssencesWrapper();

            CharactersManager manager = CharactersManager.Instance;
            manager.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestCharacters.xml"));

            Assert.AreEqual(7, manager.Characters.Count);
            Assert.AreEqual("Louga", manager.Characters[0].Name);
            Assert.AreEqual(48, manager.Characters[1].Stats.MaxMana);
            Assert.AreEqual(13, manager.Characters[2].EssenceAffinity.Id);
            Assert.AreEqual(11, manager.Characters[3].Stats.BaseDefense);
            Assert.AreEqual(6, manager.Characters[4].Stats.BaseLuck);
            Assert.AreEqual(326, manager.Characters[5].Stats.MaxHealth);
            Assert.AreEqual(2, manager.Characters[6].Stats.BaseMagicDefense);
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
