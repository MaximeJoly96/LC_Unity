using NUnit.Framework;
using Engine.Actor;
using Abilities;
using Actors;
using Party;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Inventory;
using Essence;

namespace Testing.Engine.Actor
{
    public class ChangeEquipmentTests : TestFoundation
    {
        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
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
        public void ItemWithGivenIdCanBeEquippedToCharacter()
        {
            ChangeEquipment change = new ChangeEquipment
            {
                ItemId = 1216,
                CharacterId = 0
            };

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedWeapons(new List<TextAsset>
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestWeapons.xml")
            });
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestArmours.xml"));
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestAccessories.xml"));

            global::Actors.Character character = CharactersManager.Instance.GetCharacter(0);
            PartyManager.Instance.GetParty().Add(character);

            Assert.AreEqual(-1, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(-1, character.Equipment.Head.ItemId);
            Assert.AreEqual(-1, character.Equipment.Accessory.ItemId);

            change.Run();

            Assert.AreEqual(1216, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(-1, character.Equipment.Head.ItemId);
            Assert.AreEqual(-1, character.Equipment.Accessory.ItemId);

            change.ItemId = 2001;
            change.Run();

            Assert.AreEqual(1216, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(2001, character.Equipment.Head.ItemId);
            Assert.AreEqual(-1, character.Equipment.Accessory.ItemId);

            change.ItemId = 3000;
            change.Run();

            Assert.AreEqual(1216, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(2001, character.Equipment.Head.ItemId);
            Assert.AreEqual(3000, character.Equipment.Accessory.ItemId);
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
