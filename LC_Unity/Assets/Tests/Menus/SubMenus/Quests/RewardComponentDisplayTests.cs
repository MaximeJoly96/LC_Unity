using NUnit.Framework;
using Language;
using Menus.SubMenus.Quests;
using Inventory;

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
    }
}
