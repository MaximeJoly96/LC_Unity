using BattleSystem.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testing.BattleSystem.Model
{
    public class TroopTests : TestFoundation
    {
        [Test]
        public void TroopCanBeCreated()
        {
            Troop troop = new Troop(3, new List<TroopMember>(), new List<PlayerSpot>(), 5);

            Assert.AreEqual(3, troop.Id);
            Assert.AreEqual(0, troop.Members.Count);
            Assert.AreEqual(0, troop.PlayerSpots.Count);
            Assert.AreEqual(5, troop.BattlefieldId);
        }
    }
}
