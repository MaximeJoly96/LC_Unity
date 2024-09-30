using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class IsInRangeTests
    {
        [Test]
        public void CreateBasicIsInRangeTest()
        {
            IsInRange inRange = new IsInRange();

            Assert.AreEqual(0, inRange.MinTargetCount);
            Assert.AreEqual(0, inRange.MaxTargetCount);
            Assert.AreEqual(0, inRange.Range);
        }

        [Test]
        public void SetValuesTest()
        {
            IsInRange inRange = new IsInRange(3, 0, 400);

            Assert.AreEqual(3, inRange.MinTargetCount);
            Assert.AreEqual(0, inRange.MaxTargetCount);
            Assert.AreEqual(400, inRange.Range);
        }
    }
}
