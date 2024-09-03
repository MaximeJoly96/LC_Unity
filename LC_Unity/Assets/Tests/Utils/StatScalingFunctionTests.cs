using NUnit.Framework;
using Utils;

namespace Testing.Utils
{
    public class StatScalingFunctionTests
    {
        [Test]
        public void ComputeTest()
        {
            StatScalingFunction func = new StatScalingFunction(2, 2, 5);

            Assert.AreEqual(55, func.Compute(5));
        }
    }
}
