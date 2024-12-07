using NUnit.Framework;
using BattleSystem.Model;
using UnityEngine;

namespace Testing.BattleSystem.Model
{
    public class PlayerSpotTests : TestFoundation
    {
        [Test]
        public void PlayerSpotCanBeCreated()
        {
            PlayerSpot spot = new PlayerSpot(3, 3.5f, 4.7f);

            Assert.AreEqual(3, spot.Id);
            Assert.IsTrue(Mathf.Abs(spot.X - 3.5f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(spot.Y - 4.7f) < 0.01f);
        }
    }
}
