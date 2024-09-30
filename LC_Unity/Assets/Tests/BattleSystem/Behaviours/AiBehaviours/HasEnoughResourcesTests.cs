using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using UnityEngine;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class HasEnoughResourcesTests
    {
        [Test]
        public void CreateHasEnoughResourcesTest()
        {
            HasEnoughResources hasEnough = new HasEnoughResources(HasEnoughResources.AmountType.FromAbility, 35.0f, Effects.Stat.HP);

            Assert.AreEqual(HasEnoughResources.AmountType.FromAbility, hasEnough.Amount);
            Assert.IsTrue(Mathf.Abs(35.0f - hasEnough.Value) < 0.01f);
            Assert.AreEqual(Effects.Stat.HP, hasEnough.Resource);
        }
    }
}
