using NUnit.Framework;
using BattleSystem.Model;

namespace Testing.BattleSystem.Model
{
    public class BattlerTests : TestFoundation
    {
        [Test]
        public void BattlerHealthCanBeChanged()
        {
            Battler battler = new Battler(ComponentCreator.CreateDummyCharacter());
            int health = battler.Character.Stats.CurrentHealth;

            battler.ChangeHealth(20);

            Assert.AreEqual(health - 20, battler.Character.Stats.CurrentHealth);
        }
    }
}
