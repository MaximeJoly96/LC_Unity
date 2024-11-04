﻿using NUnit.Framework;
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
    public class ChangeNameTests
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
        public void CharacterNameCanBeChanged()
        {
            ChangeName change = new ChangeName
            {
                CharacterId = 2,
                Value = "newKolibri"
            };

            global::Actors.Character character = CharactersManager.Instance.GetCharacter(2);
            PartyManager.Instance.GetParty().Add(character);

            Assert.AreEqual("Kolibri", character.Name);

            change.Run();

            Assert.AreEqual("newKolibri", character.Name);
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