using RPG_Maker_VX_Ace_Import.Database.System;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class BattleStatTests
    {
        [Test]
        public void CreateBattlerStatTest()
        {
            BattlerStat stat = new BattlerStat(new StatEvolutionCurve(2.0f, 1.0f, 0.0f), CharacterStats.Health);

            Assert.IsNotNull(stat.Curve);
            Assert.IsTrue(stat.Label == CharacterStats.Health);
        }

        [Test]
        public void GetValueBasedOnLevelTest()
        {
            BattlerStat stat = new BattlerStat(new StatEvolutionCurve(2.0f, 1.0f, 0.0f), CharacterStats.Health);

            Assert.IsTrue(Mathf.Abs(stat.GetValueBasedOnLevel(2) - 10.0f) < 0.001f);
            Assert.IsFalse(Mathf.Abs(stat.GetValueBasedOnLevel(3) - 15.0f) < 0.001f); // Expected is 21
        }
    }
}

