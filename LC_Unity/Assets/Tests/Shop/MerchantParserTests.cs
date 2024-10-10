using NUnit.Framework;
using Shop;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Inventory;

namespace Testing.Shop
{
    public class MerchantParserTests
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
        public void MerchantsCanBeParsedFromFile()
        {
            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestConsumables.xml"));
            wrapper.FeedArmours(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestArmours.xml"));

            MerchantParser parser = new MerchantParser();
            List<Merchant> merchants = parser.ParseMerchants(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Shop/TestData/TestMerchants.xml"));

            Assert.AreEqual(2, merchants.Count);
            Assert.IsTrue(merchants[0].SoldItemsTypes.Contains(ItemCategory.Armour));
            Assert.AreEqual(2, merchants[0].Items.Count);
            Assert.IsTrue(merchants[1].SoldItemsTypes.Contains(ItemCategory.Consumable));
            Assert.AreEqual(3, merchants[1].Items.Count);
        }
    }
}
