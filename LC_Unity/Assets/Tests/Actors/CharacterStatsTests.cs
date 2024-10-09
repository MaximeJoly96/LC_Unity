using NUnit.Framework;
using Actors;
using Utils;
using Actors.Equipment;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEditor;

namespace Testing.Actors
{
    public class CharacterStatsTests
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
        public void CharacterStatsCanBeCreated()
        {
            CharacterStats stats = CreateBaseStats();

            Assert.AreEqual(10, stats.Exp);
            Assert.AreEqual(256, stats.MaxHealth);
            Assert.AreEqual(25, stats.MaxMana);
            Assert.AreEqual(100, stats.MaxEssence);
        }

        [Test]
        public void LevelCanBeComputedFromExperience()
        {
            CharacterStats stats = CreateBaseStats();

            stats.SetExperience(800);

            Assert.AreEqual(1, stats.Level);

            stats.GiveExperience(1800);

            Assert.AreEqual(2, stats.Level);
        }

        [Test]
        public void ExperienceNeededForNextLevelCanBeObtained()
        {
            CharacterStats stats = CreateBaseStats();
            stats.SetExperience(10000);

            Assert.AreEqual(1790, stats.GetXpForCurrentLevel());
        }

        [Test]
        public void TotalExperienceNeededForLevelCanBeObtained()
        {
            CharacterStats stats = CreateBaseStats();

            Assert.AreEqual(150310, stats.GetTotalRequiredXpForLevel(18));
        }

        [Test]
        public void CurrentHealthCanBeChanged()
        {
            CharacterStats stats = CreateBaseStats();

            stats.ChangeHealth(10);
            Assert.AreEqual(246, stats.CurrentHealth);

            stats.ChangeHealth(-10);
            Assert.AreEqual(256, stats.CurrentHealth);

            stats.ChangeHealth(-50);
            Assert.AreEqual(256, stats.CurrentHealth);

            stats.ChangeHealth(500);
            Assert.AreEqual(0, stats.CurrentHealth);
        }

        [Test]
        public void CurrentManaCanBeChanged()
        {
            CharacterStats stats = CreateBaseStats();

            stats.ChangeMana(15);
            Assert.AreEqual(10, stats.CurrentMana);

            stats.ChangeMana(-10);
            Assert.AreEqual(20, stats.CurrentMana);

            stats.ChangeMana(-50);
            Assert.AreEqual(25, stats.CurrentMana);

            stats.ChangeMana(500);
            Assert.AreEqual(0, stats.CurrentMana);
        }

        [Test]
        public void CurrentEssenceCanBeChanged()
        {
            CharacterStats stats = CreateBaseStats();

            stats.ChangeEssence(15);
            Assert.AreEqual(85, stats.CurrentEssence);

            stats.ChangeEssence(-10);
            Assert.AreEqual(95, stats.CurrentEssence);

            stats.ChangeEssence(-50);
            Assert.AreEqual(100, stats.CurrentEssence);

            stats.ChangeEssence(500);
            Assert.AreEqual(0, stats.CurrentEssence);
        }

        [Test]
        public void StatsCanBeComputedBasedOnLevel()
        {
            CharacterStats stats = CreateBaseStats();

            Assert.AreEqual(256, stats.MaxHealth);
            Assert.AreEqual(25, stats.MaxMana);
            Assert.AreEqual(100, stats.MaxEssence);
            Assert.AreEqual(8, stats.BaseStrength);
            Assert.AreEqual(7, stats.BaseDefense);
            Assert.AreEqual(3, stats.BaseMagic);
            Assert.AreEqual(8, stats.BaseMagicDefense);
            Assert.AreEqual(5, stats.BaseAgility);
            Assert.AreEqual(6, stats.BaseLuck);

            stats.GiveExperience(2600);

            Assert.AreEqual(307, stats.MaxHealth);
            Assert.AreEqual(35, stats.MaxMana);
            Assert.AreEqual(100, stats.MaxEssence);
            Assert.AreEqual(11, stats.BaseStrength);
            Assert.AreEqual(10, stats.BaseDefense);
            Assert.AreEqual(5, stats.BaseMagic);
            Assert.AreEqual(11, stats.BaseMagicDefense);
            Assert.AreEqual(8, stats.BaseAgility);
            Assert.AreEqual(13, stats.BaseLuck);
        }

        [Test]
        public void BonusStatsCanBeObtained()
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

            CharacterEquipment equipment = new CharacterEquipment();
            equipment.Init();
            equipment.RightHand = new EquipmentSlot(EquipmentPosition.RightHand, 1205);
            equipment.Accessory = new EquipmentSlot(EquipmentPosition.Accessory, 3000);

            CharacterStats stats = CreateBaseStats(equipment);

            Assert.AreEqual(26, stats.BonusHealth);
            Assert.AreEqual(282, stats.MaxHealth);
            Assert.AreEqual(130, stats.BonusMana);
            Assert.AreEqual(155, stats.MaxMana);
            Assert.AreEqual(35, stats.BonusStrength);
            Assert.AreEqual(43, stats.BaseStrength + stats.BonusStrength);
            Assert.AreEqual(72, stats.BonusMagic);
            Assert.AreEqual(75, stats.BaseMagic + stats.BonusMagic);

            stats.GiveExperience(2600);

            Assert.AreEqual(31, stats.BonusHealth);
            Assert.AreEqual(338, stats.MaxHealth);
            Assert.AreEqual(130, stats.BonusMana);
            Assert.AreEqual(165, stats.MaxMana);
            Assert.AreEqual(35, stats.BonusStrength);
            Assert.AreEqual(46, stats.BaseStrength + stats.BonusStrength);
            Assert.AreEqual(72, stats.BonusMagic);
            Assert.AreEqual(77, stats.BaseMagic + stats.BonusMagic);
        }

        [Test]
        public void SecondaryStatsCanBeObtained()
        {
            CharacterStats stats = CreateBaseStats();

            Assert.AreEqual(5, stats.CritChance);
            Assert.AreEqual(100, stats.CritDamage);
            Assert.AreEqual(10, stats.Parry);
            Assert.AreEqual(5, stats.Evasion);
            Assert.AreEqual(100, stats.Provocation);
            Assert.AreEqual(90, stats.Accuracy);
        }

        private CharacterStats CreateBaseStats()
        {
            CharacterEquipment equipment = new CharacterEquipment();
            equipment.Init();

            return CreateBaseStats(equipment);
        }

        private CharacterStats CreateBaseStats(CharacterEquipment equipment)
        {
            return new CharacterStats(new QuadraticFunction(450, 250, 10),
                                      new StatScalingFunction(1.5f, 1.2f, 8),
                                      new StatScalingFunction(1.4f, 1.1f, 7),
                                      new StatScalingFunction(0.9f, 1.2f, 3),
                                      new StatScalingFunction(1.2f, 1.2f, 8),
                                      new StatScalingFunction(1.7f, 0.975f, 5),
                                      new StatScalingFunction(4, 0.85f, 6),
                                      new StatScalingFunction(22, 1.2f, 256),
                                      new StatScalingFunction(6, 0.8f, 25),
                                      new StatScalingFunction(0, 1.0f, 100),
                                      equipment);
        }
    }
}
