using NUnit.Framework;
using Core.Model;
using Inventory;

namespace Testing.Inventory
{
    public class AccessoryTests : TestFoundation
    {
        [Test]
        public void AccessoryCanBeCreated()
        {
            Accessory accessory = new Accessory(new ElementIdentifier(5, "acc", "accDesc"), 3, 8, ItemCategory.Accessory, 6);

            Assert.AreEqual(5, accessory.Id);
            Assert.AreEqual("acc", accessory.Name);
            Assert.AreEqual("accDesc", accessory.Description);
            Assert.AreEqual(3, accessory.Icon);
            Assert.AreEqual(8, accessory.Price);
            Assert.AreEqual(ItemCategory.Accessory, accessory.Category);
            Assert.AreEqual(6, accessory.EnchantmentSlots);
        }
    }
}
