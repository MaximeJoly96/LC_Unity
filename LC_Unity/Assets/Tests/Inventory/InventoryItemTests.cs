using NUnit.Framework;
using Core.Model;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

namespace Testing.Inventory
{
    public class InventoryItemTests : TestFoundation
    {
        [Test]
        public void CanCreateInventoryItemFromBaseItem()
        {
            InventoryItem item = new InventoryItem(new BaseItem(new ElementIdentifier(3, "name", "desc"), 3, 4, ItemCategory.Resource));

            Assert.AreEqual(3, item.ItemData.Id);
            Assert.AreEqual("name", item.ItemData.Name);
            Assert.AreEqual("desc", item.ItemData.Description);
            Assert.AreEqual(3, item.ItemData.Icon);
            Assert.AreEqual(4, item.ItemData.Price);
            Assert.AreEqual(ItemCategory.Resource, item.ItemData.Category);
            Assert.AreEqual(0, item.InPossession);
        }

        [Test]
        public void CanCreateInventoryItemFromIdOnly()
        {
            ItemsWrapper wrapper = CreateEmptyItemWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestConsumables.xml"));

            InventoryItem item = new InventoryItem(9, 5);

            Assert.AreEqual("bandage", item.ItemData.Name);
            Assert.AreEqual(5, item.InPossession);
        }

        [Test]
        public void ItemAmountCanBeChanged()
        {
            InventoryItem item = new InventoryItem(new BaseItem(new ElementIdentifier(3, "name", "desc"), 3, 4, ItemCategory.Resource));

            item.ChangeAmount(5);
            Assert.AreEqual(5, item.InPossession);

            item.ChangeAmount(-4);
            Assert.AreEqual(1, item.InPossession);

            LogAssert.ignoreFailingMessages = true;
            item.ChangeAmount(-50);
            LogAssert.ignoreFailingMessages = false;
            Assert.AreEqual(1, item.InPossession);

            item.ChangeAmount(600);
            Assert.AreEqual(99, item.InPossession);
        }

        [Test]
        public void InventoryItemCanBeSerialized()
        {
            InventoryItem item = new InventoryItem(new BaseItem(new ElementIdentifier(3, "name", "desc"), 3, 4, ItemCategory.Resource));
            item.ChangeAmount(42);

            Assert.AreEqual("42", item.Serialize());
        }

        [Test]
        public void InventoryItemCanBeDeserialized()
        {
            ItemsWrapper wrapper = CreateEmptyItemWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Inventory/TestData/TestConsumables.xml"));

            InventoryItem item = InventoryItem.Deserialize("item4", "18");

            Assert.AreEqual(4, item.ItemData.Id);
            Assert.AreEqual(18, item.InPossession);
        }

        private ItemsWrapper CreateEmptyItemWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
