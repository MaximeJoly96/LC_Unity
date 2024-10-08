using NUnit.Framework;
using Party;
using Engine.Party;

namespace Testing.Party
{
    public class PartyManagerTests
    {
        [SetUp]
        public void Setup()
        {
            PartyManager.Instance.Inventory.Clear();
            PartyManager.Instance.GetParty().Clear();
        }

        [Test]
        public void PartyManagerCanBeCreated()
        {
            PartyManager manager = PartyManager.Instance;

            Assert.AreEqual(0, manager.Gold);
            Assert.AreEqual(0, manager.Inventory.Count);
            Assert.AreEqual(0, manager.GetParty().Count);
        }

        [Test]
        public void GoldValueCanBeChanged()
        {
            PartyManager manager = PartyManager.Instance;

            Assert.AreEqual(0, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = 100 });

            Assert.AreEqual(100, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = -50 });

            Assert.AreEqual(50, manager.Gold);
            manager.ChangeGold(new ChangeGold { Value = -200 });

            Assert.AreEqual(0, manager.Gold);
        }
    }
}
