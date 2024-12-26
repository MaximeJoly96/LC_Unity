using NUnit.Framework;
using Actors;
using Actors.Equipment;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEditor;
using Language;

namespace Testing.Actors.Equipment
{
    public class EquipmentSlotTests : TestFoundation
    {
        [Test]
        public void EquipmentSlotCanBeCreated()
        {
            EquipmentSlot leftHandSlot = new EquipmentSlot(EquipmentPosition.LeftHand, 0);
            EquipmentSlot rightHandSlot = new EquipmentSlot(EquipmentPosition.RightHand, 1);
            EquipmentSlot headSlot = new EquipmentSlot(EquipmentPosition.Helmet, 2);
            EquipmentSlot bodySlot = new EquipmentSlot(EquipmentPosition.Body, 3);
            EquipmentSlot accessorySlot = new EquipmentSlot(EquipmentPosition.Accessory, 4);

            Assert.AreEqual(0, leftHandSlot.ItemId);
            Assert.AreEqual(EquipmentPosition.RightHand, rightHandSlot.Position);
            Assert.AreEqual(2, headSlot.ItemId);
            Assert.AreEqual(EquipmentPosition.Body, bodySlot.Position);
            Assert.AreEqual(4, accessorySlot.ItemId);
        }

        [Test]
        public void ItemNameCanBeObtained()
        {
            GameObject localizer = new GameObject("Localizer");
            _usedGameObjects.Add(localizer);
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/french.csv") };
            component.LoadLanguage(global::Language.Language.French, files);

            EquipmentSlot headSlot = new EquipmentSlot(EquipmentPosition.Helmet, 2001);
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));

            Assert.AreEqual("Casque en cuir", headSlot.Name);
        }

        [Test]
        public void EquipmentItemCanBeObtainedFromSlot()
        {
            EquipmentSlot headSlot = new EquipmentSlot(EquipmentPosition.Helmet, 2001);
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Actors/TestData/TestArmours.xml"));

            EquipmentItem item = headSlot.GetItem();

            Assert.AreEqual(2001, item.Id);
            Assert.AreEqual("leatherHelmet", item.Name);
            Assert.AreEqual("leatherHelmetDescription", item.Description);
            Assert.AreEqual(0, item.Icon);
            Assert.AreEqual(10, item.Price);
            Assert.AreEqual(ArmourType.Head, (item as Armour).Type);
            Assert.AreEqual(3, item.EnchantmentSlots);
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
