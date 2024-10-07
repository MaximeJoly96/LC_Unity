using NUnit.Framework;
using Actors.Equipment;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEditor;
using Effects;

namespace Testing.Actors.Equipment
{
    public class CharacterEquipmentTests
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
        public void InitialEquipmentCanBeDefined()
        {
            CharacterEquipment equipment = new CharacterEquipment();
            equipment.Init();

            Assert.AreEqual(-1, equipment.RightHand.ItemId);
            Assert.AreEqual(-1, equipment.LeftHand.ItemId);
            Assert.AreEqual(-1, equipment.Head.ItemId);
            Assert.AreEqual(-1, equipment.Body.ItemId);
            Assert.AreEqual(-1, equipment.Accessory.ItemId);

            Assert.AreEqual(EquipmentPosition.RightHand, equipment.RightHand.Position);
            Assert.AreEqual(EquipmentPosition.LeftHand, equipment.LeftHand.Position);
            Assert.AreEqual(EquipmentPosition.Helmet, equipment.Head.Position);
            Assert.AreEqual(EquipmentPosition.Body, equipment.Body.Position);
            Assert.AreEqual(EquipmentPosition.Accessory, equipment.Accessory.Position);
        }

        [Test]
        public void AllEffectsFromEquippedItemsCanBeRetrieved()
        {
            CharacterEquipment equipment = new CharacterEquipment();

            equipment.RightHand = new EquipmentSlot(EquipmentPosition.RightHand, 1071);
            equipment.LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, -1);
            equipment.Head = new EquipmentSlot(EquipmentPosition.Helmet, 2001);
            equipment.Body = new EquipmentSlot(EquipmentPosition.Body, 2000);
            equipment.Accessory = new EquipmentSlot(EquipmentPosition.Accessory, 3000);

            ItemsWrapper wrapper = CreateEmptyWrapper();

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestWeapons.xml")
            };
            wrapper.FeedWeapons(files);
            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestAccessories.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestArmours.xml"));
            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestKeyItems.xml"));
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestConsumables.xml"));
            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestResources.xml"));

            List<IEffect> effects = equipment.GetAllItemEffects();

            Assert.AreEqual(2, effects.Count);
            Assert.IsTrue(effects[0] is AdditionalStrike);
            Assert.IsTrue(effects[1] is StatBoost);
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
