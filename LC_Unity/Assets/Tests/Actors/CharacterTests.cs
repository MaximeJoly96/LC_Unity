using NUnit.Framework;
using Actors;
using Abilities;
using UnityEngine;
using System.Linq;
using Core.Model;
using Utils;
using Inventory;
using System.Collections.Generic;
using UnityEditor;

namespace Testing.Actors
{
    public class CharacterTests
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

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }

        [Test]
        public void ParameterlessCharacterCanBeCreated()
        {
            AbilitiesManager.Instance.Abilities.Clear();
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(0, "attackCommand", "attackCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.AttackCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(1, "fleeCommand", "fleeCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.FleeCommand, -1));

            Character character = new Character();

            Assert.AreEqual(2, character.Abilities.Count);
            Assert.AreEqual(10, character.ElementalAffinities.Count);
            Assert.AreEqual(0, character.ActiveEffects.Count);

            Assert.AreEqual(AbilityCategory.AttackCommand, character.Abilities[0].Category);
            Assert.AreEqual(AbilityCategory.FleeCommand, character.Abilities[1].Category);

            Assert.AreEqual(-1, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(-1, character.Equipment.LeftHand.ItemId);
            Assert.AreEqual(-1, character.Equipment.Head.ItemId);
            Assert.AreEqual(-1, character.Equipment.Body.ItemId);
            Assert.AreEqual(-1, character.Equipment.Accessory.ItemId);

            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Neutral).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Fire).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Thunder).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Water).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Ice).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Earth).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Wind).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Holy).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Darkness).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.ElementalAffinities.FirstOrDefault(e => e.Element == Element.Healing).Multiplier) < 0.01f);
        }

        [Test]
        public void CharacterWithFunctionsCanBeCreated()
        {
            AbilitiesManager.Instance.Abilities.Clear();
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(0, "attackCommand", "attackCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.AttackCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(1, "fleeCommand", "fleeCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.FleeCommand, -1));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.AreEqual(AbilityCategory.AttackCommand, character.Abilities[0].Category);
            Assert.AreEqual(AbilityCategory.FleeCommand, character.Abilities[1].Category);

            Assert.AreEqual(10, character.Stats.Exp);
            Assert.AreEqual(256, character.Stats.MaxHealth);
            Assert.AreEqual(25, character.Stats.MaxMana);
            Assert.AreEqual(100, character.Stats.MaxEssence);
            Assert.AreEqual(8, character.Stats.BaseStrength);
            Assert.AreEqual(7, character.Stats.BaseDefense);
            Assert.AreEqual(3, character.Stats.BaseMagic);
            Assert.AreEqual(8, character.Stats.BaseMagicDefense);
            Assert.AreEqual(5, character.Stats.BaseAgility);
            Assert.AreEqual(6, character.Stats.BaseLuck);
        }

        [Test]
        public void LevelCanBeChanged()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            character.ChangeLevel(3);
            Assert.AreEqual(3, character.Stats.Level);

            character.ChangeLevel(4);
            Assert.AreEqual(7, character.Stats.Level);

            character.ChangeLevel(-2);
            Assert.AreEqual(5, character.Stats.Level);
        }

        [Test]
        public void ExperienceCanBeGiven()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.AreEqual(10, character.Stats.Exp);
            character.GiveExp(1500);
            Assert.AreEqual(1510, character.Stats.Exp);
            character.GiveExp(-300);
            Assert.AreEqual(1210, character.Stats.Exp);
        }

        [Test]
        public void ResourcesCanBeRecovered()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            character.ChangeHealth(50);

            Assert.AreEqual(206, character.Stats.CurrentHealth);

            character.Recover();

            Assert.AreEqual(256, character.Stats.CurrentHealth);
            Assert.AreEqual(25, character.Stats.CurrentMana);
        }

        [Test]
        public void EquipmentCanBeChanged()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestWeapons.xml")
            };
            wrapper.FeedWeapons(files);
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));
            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestKeyItems.xml"));
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestConsumables.xml"));
            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestResources.xml"));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            character.ChangeEquipment(1216);

            Assert.AreEqual("sickle", character.Equipment.RightHand.GetItem().Name);
        }

        [Test]
        public void AbilityCanBeLearnt()
        {
            AbilitiesManager.Instance.Abilities.Clear();
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(0, "attackCommand", "attackCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.AttackCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(1, "fleeCommand", "fleeCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.FleeCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(2, "magicka", "magickaDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.Skill, -1));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.AreEqual(2, character.Abilities.Count);

            character.LearnSkill(2);

            Assert.AreEqual(3, character.Abilities.Count);
            Assert.AreEqual("magicka", character.Abilities[2].Name);
        }

        [Test]
        public void AbilityCanBeForgotten()
        {
            AbilitiesManager.Instance.Abilities.Clear();
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(0, "attackCommand", "attackCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.AttackCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(1, "fleeCommand", "fleeCommandDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.FleeCommand, -1));
            AbilitiesManager.Instance.AddAbility(new Ability(new ElementIdentifier(2, "magicka", "magickaDescription"),
                                                             0, AbilityUsability.BattleOnly, TargetEligibility.Any, AbilityCategory.Skill, -1));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.AreEqual(2, character.Abilities.Count);

            character.ForgetSkill(0);

            Assert.AreEqual(1, character.Abilities.Count);
            Assert.AreEqual(AbilityCategory.FleeCommand, character.Abilities[0].Category);

            character.ForgetSkill(2);

            Assert.AreEqual(1, character.Abilities.Count);
        }

        [Test]
        public void ElementalAffinityCanBeObtainedFromElement()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.IsTrue(Mathf.Abs(1.0f - character.GetElementalAffinity(Element.Fire).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.GetElementalAffinity(Element.Neutral).Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(1.0f - character.GetElementalAffinity(Element.Wind).Multiplier) < 0.01f);
        }

        [Test]
        public void CanCheckIfItemIsEquipped()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestWeapons.xml")
            };
            wrapper.FeedWeapons(files);
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));
            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestKeyItems.xml"));
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestConsumables.xml"));
            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestResources.xml"));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.IsFalse(character.HasItemEquipped(1216));

            character.ChangeEquipment(1216);

            Assert.IsTrue(character.HasItemEquipped(1216));
            Assert.IsFalse(character.HasItemEquipped(1071));
        }

        [Test]
        public void CharacterNameCanBeUpdated()
        {
            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            Assert.AreEqual("Louga", character.Name);

            character.UpdateName("Agrid");

            Assert.AreEqual("Agrid", character.Name);
        }

        [Test]
        public void CharacterCanBeSerialized()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestWeapons.xml")
            };
            wrapper.FeedWeapons(files);
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));
            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestKeyItems.xml"));
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestConsumables.xml"));
            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestResources.xml"));

            Character character = new Character(new ElementIdentifier(0, "Louga", ""),
                                                new QuadraticFunction(450, 250, 10),
                                                new StatScalingFunction(22, 1.2f, 256),
                                                new StatScalingFunction(6, 0.8f, 25),
                                                new StatScalingFunction(0, 1.0f, 100),
                                                new StatScalingFunction(1.5f, 1.2f, 8),
                                                new StatScalingFunction(1.4f, 1.1f, 7),
                                                new StatScalingFunction(0.9f, 1.2f, 3),
                                                new StatScalingFunction(1.2f, 1.2f, 8),
                                                new StatScalingFunction(1.7f, 0.975f, 5),
                                                new StatScalingFunction(4, 0.85f, 6));

            character.GiveExp(1500);
            character.ChangeEquipment(1216);
            character.ChangeEquipment(3000);
            character.ChangeEquipment(2000);
            character.ChangeEquipment(2001);

            Assert.AreEqual("1510,2001,-1,1216,2000,3000", character.Serialize());
        }

        [Test]
        public void CharacterCanBeDeserialized()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestWeapons.xml")
            };
            wrapper.FeedWeapons(files);
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));
            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestKeyItems.xml"));
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestConsumables.xml"));
            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestResources.xml"));

            CharactersManager.Instance.AddCharacter(new Character(new ElementIdentifier(0, "Louga", ""),
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

            Character character = Character.Deserialize("character0", "1510,2001,-1,1216,2000,3000");

            Assert.AreEqual(0, character.Id);
            Assert.AreEqual(1510, character.Stats.Exp);
            Assert.AreEqual(1216, character.Equipment.RightHand.ItemId);
            Assert.AreEqual(-1, character.Equipment.LeftHand.ItemId);
            Assert.AreEqual(2001, character.Equipment.Head.ItemId);
            Assert.AreEqual(2000, character.Equipment.Body.ItemId);
            Assert.AreEqual(3000, character.Equipment.Accessory.ItemId);
        }
    }
}
