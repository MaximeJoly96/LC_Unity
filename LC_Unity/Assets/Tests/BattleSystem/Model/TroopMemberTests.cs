using NUnit.Framework;
using BattleSystem.Model;
using UnityEngine;

namespace Testing.BattleSystem.Model
{
    public class TroopMemberTests : TestFoundation
    {
        [Test]
        public void TroopMemberCanBeCreated()
        {
            TroopMember member = new TroopMember(4, -2.3f, 4.6f);

            Assert.AreEqual(4, member.Id);
            Assert.IsTrue(Mathf.Abs(member.X + 2.3f) < 0.01f);
            Assert.IsTrue(Mathf.Abs(member.Y - 4.6f) < 0.01f);
        }
    }
}
