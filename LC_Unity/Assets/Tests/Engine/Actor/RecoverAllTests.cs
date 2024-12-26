using NUnit.Framework;
using Engine.Actor;
using Party;
using Actors;
using UnityEditor;
using UnityEngine;
using Engine.Party;
using Abilities;
using Essence;
using System.Collections.Generic;

namespace Testing.Engine.Actor
{
    public class RecoverAllTests : TestFoundation
    {
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
        public void RecoverAllHealsAllCharacters()
        {
            global::Actors.Character character1 = CharactersManager.Instance.GetCharacter(1);
            global::Actors.Character character2 = CharactersManager.Instance.GetCharacter(2);

            PartyManager.Instance.GetParty().Add(character1);
            PartyManager.Instance.GetParty().Add(character2);

            Assert.AreEqual(188, character1.Stats.CurrentHealth);
            Assert.AreEqual(48, character1.Stats.CurrentMana);
            Assert.AreEqual(100, character1.Stats.CurrentEssence);
            Assert.AreEqual(210, character2.Stats.CurrentHealth);
            Assert.AreEqual(31, character2.Stats.CurrentMana);
            Assert.AreEqual(100, character2.Stats.CurrentEssence);

            character1.ChangeHealth(20);
            character1.ChangeMana(10);
            character1.ChangeEssence(30);
            character2.ChangeHealth(40);
            character2.ChangeMana(35);
            character2.ChangeEssence(50);

            Assert.AreEqual(168, character1.Stats.CurrentHealth);
            Assert.AreEqual(38, character1.Stats.CurrentMana);
            Assert.AreEqual(70, character1.Stats.CurrentEssence);
            Assert.AreEqual(170, character2.Stats.CurrentHealth);
            Assert.AreEqual(0, character2.Stats.CurrentMana);
            Assert.AreEqual(50, character2.Stats.CurrentEssence);

            RecoverAll recover = new RecoverAll();
            recover.Run();

            Assert.AreEqual(188, character1.Stats.CurrentHealth);
            Assert.AreEqual(48, character1.Stats.CurrentMana);
            Assert.AreEqual(70, character1.Stats.CurrentEssence);
            Assert.AreEqual(210, character2.Stats.CurrentHealth);
            Assert.AreEqual(31, character2.Stats.CurrentMana);
            Assert.AreEqual(50, character2.Stats.CurrentEssence);
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
