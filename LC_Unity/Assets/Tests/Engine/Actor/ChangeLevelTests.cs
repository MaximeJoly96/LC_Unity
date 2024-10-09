using NUnit.Framework;
using Engine.Actor;
using Abilities;
using Actors;
using Party;
using UnityEditor;
using UnityEngine;
using Essence;
using System.Collections.Generic;

namespace Testing.Engine.Actor
{
    public class ChangeLevelTests
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
            PartyManager.Instance.GetParty().Clear();
            CharactersManager.Instance.Characters.Clear();
            AbilitiesManager.Instance.Abilities.Clear();

            CreateEssencesWrapper();
            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestCharacters.xml"));
            AbilitiesManager.Instance.Init(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestAbilities.xml"));
        }

        [Test]
        public void CharacterLevelCanBeChanged()
        {
            ChangeLevel change = new ChangeLevel
            {
                CharacterId = 1,
                Amount = 3
            };

            global::Actors.Character character = CharactersManager.Instance.GetCharacter(1);
            PartyManager.Instance.GetParty().Add(character);

            Assert.AreEqual(0, character.Stats.Level);

            change.Run();

            Assert.AreEqual(3, character.Stats.Level);

            change.Amount = -2;
            change.Run();

            Assert.AreEqual(1, character.Stats.Level);
        }

        private EssencesWrapper CreateEssencesWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            EssencesWrapper wrapper = go.AddComponent<EssencesWrapper>();
            wrapper.FeedAffinities(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/Essences.xml"));

            return wrapper;
        }
    }
}
