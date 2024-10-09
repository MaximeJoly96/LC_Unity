using NUnit.Framework;
using Engine.Actor;
using Actors;
using Party;
using UnityEditor;
using UnityEngine;
using Abilities;
using Essence;
using System.Collections.Generic;

namespace Testing.Engine.Actor
{
    public class ChangeSkillsTests
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
            AbilitiesManager.Instance.Init(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestAbilities.xml"));
            CharactersManager.Instance.LoadCharactersFromFile(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestCharacters.xml"));
        }

        [Test]
        public void ChangeSkillsCanLearnAbilityToCharacter()
        {
            ChangeSkills change = new ChangeSkills
            {
                Action = ChangeSkills.ActionType.Learn,
                CharacterId = 0,
                SkillId = 5
            };

            global::Actors.Character character = CharactersManager.Instance.GetCharacter(0);
            PartyManager.Instance.GetParty().Add(character);

            Assert.AreEqual(2, character.Abilities.Count);
            Assert.AreEqual(0, character.Abilities[0].Id);
            Assert.AreEqual(1, character.Abilities[1].Id);

            change.Run();

            Assert.AreEqual(3, character.Abilities.Count);
            Assert.AreEqual(0, character.Abilities[0].Id);
            Assert.AreEqual(1, character.Abilities[1].Id);
            Assert.AreEqual(5, character.Abilities[2].Id);
        }

        [Test]
        public void ChangeSkillsCanMakeCharacterForgetAbility()
        {
            ChangeSkills change = new ChangeSkills
            {
                Action = ChangeSkills.ActionType.Forget,
                CharacterId = 1,
                SkillId = 1
            };

            global::Actors.Character character = CharactersManager.Instance.GetCharacter(1);
            PartyManager.Instance.GetParty().Add(character);

            Assert.AreEqual(2, character.Abilities.Count);
            Assert.AreEqual(0, character.Abilities[0].Id);
            Assert.AreEqual(1, character.Abilities[1].Id);

            change.Run();

            Assert.AreEqual(1, character.Abilities.Count);
            Assert.AreEqual(0, character.Abilities[0].Id);
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
