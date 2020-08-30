using RPG_Maker_VX_Ace_Import.Database.Battlers;
using RPG_Maker_VX_Ace_Import.Database.System;
using NUnit.Framework;

namespace Tests
{
    public class BattlerTests
    {
        [Test]
        public void CreateBattlerTest()
        {
            Battler battler = new Battler(3);

            Assert.IsNotNull(battler.Stats);
        }

        [Test]
        public void AddNewStatTest()
        {
            Battler battler = new Battler(5);

            int count = battler.Stats.Count;

            battler.AddNewStat(new BattlerStat(new StatEvolutionCurve(1.0f, 1.0f, 1.0f), CharacterStats.Agility));
            Assert.AreEqual(count + 1, battler.Stats.Count);
        }
    }
}

