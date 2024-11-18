using NUnit.Framework;
using Language;
using Menus.SubMenus.Quests;
using Inventory;
using UnityEngine;

namespace Testing.Menus.SubMenus.Quests
{
    public class RewardComponentDisplayTests : TestFoundation
    {
        [Test]
        public void ComponentCanBeInitedWithGold()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            RewardComponentDisplay display = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(display.gameObject);

            display.Init(150, SingleRewardType.Gold);

            Assert.AreEqual("150", display.Quantity.text);
            Assert.AreEqual("Okanes", display.Label.text);

            display.Init(1, SingleRewardType.Gold);

            Assert.AreEqual("1", display.Quantity.text);
            Assert.AreEqual("Okane", display.Label.text);
        }

        [Test]
        public void ComponentCanBeInitedWithExperience()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            RewardComponentDisplay display = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(display.gameObject);

            display.Init(159, SingleRewardType.Experience);

            Assert.AreEqual("159", display.Quantity.text);
            Assert.AreEqual("XP", display.Label.text);
        }

        [Test]
        public void ComponentCanBeInitedWithItem()
        {
            ItemsWrapper wrapper = ComponentCreator.CreateItemsWrapper("Menus/SubMenus/Quests/TestData/Consumables.xml");
            _usedGameObjects.Add(wrapper.gameObject);

            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            RewardComponentDisplay display = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(display.gameObject);

            display.Init(new InventoryItem(0, 3));

            Assert.AreEqual("3", display.Quantity.text);
            Assert.AreEqual("Potion", display.Label.text);
        }

        [Test]
        public void VisualStatusCanBeUpdated()
        {
            RewardComponentDisplay display = ComponentCreator.CreateRewardComponentDisplay();
            _usedGameObjects.Add(display.gameObject);

            Assert.IsTrue(Mathf.Abs(Color.white.r - display.Label.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.g - display.Label.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.b - display.Label.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.a - display.Label.color.a) < 0.01f);

            Assert.IsTrue(Mathf.Abs(Color.white.r - display.Quantity.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.g - display.Quantity.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.b - display.Quantity.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.white.a - display.Quantity.color.a) < 0.01f);

            display.UpdateVisualStatus(Color.green);

            Assert.IsTrue(Mathf.Abs(Color.green.r - display.Label.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.g - display.Label.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.b - display.Label.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.a - display.Label.color.a) < 0.01f);

            Assert.IsTrue(Mathf.Abs(Color.green.r - display.Quantity.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.g - display.Quantity.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.b - display.Quantity.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(Color.green.a - display.Quantity.color.a) < 0.01f);
        }
    }
}
