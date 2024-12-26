using NUnit.Framework;
using UnityEngine;
using Inventory;
using System.Collections.Generic;
using UnityEditor;

namespace Testing.Inventory
{
    public class ItemsWrapperTests : TestFoundation
    {
        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }

        [Test]
        public void WeaponsCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedWeapons(null);
            Assert.IsNull(wrapper.Weapons);

            List<TextAsset> files = new List<TextAsset>()
            {
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestWeapons.xml")
            };

            wrapper.FeedWeapons(files);
            Assert.AreEqual(3, wrapper.Weapons.Count);
        }

        [Test]
        public void AccessoriesCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedAccessories(null);
            Assert.IsNull(wrapper.Accessories);

            wrapper.FeedAccessories(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestAccessories.xml"));
            Assert.AreEqual(1, wrapper.Accessories.Count);
        }

        [Test]
        public void ArmoursCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedArmours(null);
            Assert.IsNull(wrapper.Armours);

            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestArmours.xml"));
            Assert.AreEqual(3, wrapper.Armours.Count);
        }

        [Test]
        public void KeyItemsCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedKeyItems(null);
            Assert.IsNull(wrapper.KeyItems);

            wrapper.FeedKeyItems(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestKeyItems.xml"));
            Assert.AreEqual(1, wrapper.KeyItems.Count);
        }

        [Test]
        public void ConsumablesCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedConsumables(null);
            Assert.IsNull(wrapper.Consumables);

            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestConsumables.xml"));
            Assert.AreEqual(3, wrapper.Consumables.Count);
        }

        [Test]
        public void ResourcesCanBeFed()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();

            wrapper.FeedResources(null);
            Assert.IsNull(wrapper.Resources);

            wrapper.FeedResources(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestResources.xml"));
            Assert.AreEqual(1, wrapper.Resources.Count);
        }

        [Test]
        public void CanGetItemFromId()
        {
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

            Assert.AreEqual("sharur", wrapper.GetItemFromId(1205).Name);
            Assert.AreEqual(3, (wrapper.GetItemFromId(3000) as Accessory).EnchantmentSlots);
            Assert.AreEqual(ArmourType.Head, (wrapper.GetItemFromId(2001) as Armour).Type);
            Assert.AreEqual(ItemUsability.Always, (wrapper.GetItemFromId(4) as Consumable).Usability);
            Assert.AreEqual("sealedMedallionDescription", wrapper.GetItemFromId(5000).Description);
            Assert.AreEqual(10, wrapper.GetItemFromId(4000).Price);
        }
    }
}
