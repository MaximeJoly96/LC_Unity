using Core.Model;
using Inventory;
using NUnit.Framework;
using Abilities;

namespace Testing.Inventory
{
    public class ConsumableTests
    {
        [Test]
        public void ConsumableCanBeCreated()
        {
            ElementIdentifier identifier = new ElementIdentifier(2, "cons", "consDesc");
            AbilityAnimation animation = new AbilityAnimation("channel", "strike", 10, 12, 15);
            Consumable cons = new Consumable(identifier, 5, 8, ItemCategory.Consumable, ItemUsability.BattleOnly, 5, animation);

            Assert.AreEqual(2, cons.Id);
            Assert.AreEqual("cons", cons.Name);
            Assert.AreEqual("consDesc", cons.Description);
            Assert.AreEqual(5, cons.Icon);
            Assert.AreEqual(8, cons.Price);
            Assert.AreEqual(ItemCategory.Consumable, cons.Category);
            Assert.AreEqual(ItemUsability.BattleOnly, cons.Usability);
            Assert.AreEqual(5, cons.Priority);
            Assert.AreEqual("channel", cons.Animation.BattlerChannelAnimationName);
            Assert.AreEqual("strike", cons.Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(10, cons.Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual(12, cons.Animation.ImpactAnimationParticlesId);
        }
    }
}
