using NUnit.Framework;
using Core.Model;
using Inventory;

namespace Testing.Inventory
{
    public class ArmourTests : TestFoundation
    {
        [Test]
        public void ArmourCanBeCreated()
        {
            Armour armour = new Armour(new ElementIdentifier(8, "armour", "armourDesc"), 9, 15, ItemCategory.Armour, 2, ArmourType.Body);

            Assert.AreEqual(8, armour.Id);
            Assert.AreEqual("armour", armour.Name);
            Assert.AreEqual("armourDesc", armour.Description);
            Assert.AreEqual(9, armour.Icon);
            Assert.AreEqual(15, armour.Price);
            Assert.AreEqual(ItemCategory.Armour, armour.Category);
            Assert.AreEqual(2, armour.EnchantmentSlots);
            Assert.AreEqual(ArmourType.Body, armour.Type);
        }
    }
}
