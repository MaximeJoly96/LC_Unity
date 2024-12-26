using Core.Model;
using NUnit.Framework;
using Inventory;

namespace Testing.Inventory
{
    public class KeyItemTests : TestFoundation
    {
        [Test]
        public void KeyItemCanBeCreated()
        {
            KeyItem keyItem = new KeyItem(new ElementIdentifier(8, "key", "item"), 2, 5, ItemCategory.KeyItem, ItemUsability.MenuOnly);

            Assert.AreEqual(8, keyItem.Id);
            Assert.AreEqual("key", keyItem.Name);
            Assert.AreEqual("item", keyItem.Description);
            Assert.AreEqual(2, keyItem.Icon);
            Assert.AreEqual(5, keyItem.Price);
            Assert.AreEqual(ItemCategory.KeyItem, keyItem.Category);
            Assert.AreEqual(ItemUsability.MenuOnly, keyItem.Usability);
        }
    }
}
