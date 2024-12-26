using Utils;
using NUnit.Framework;

namespace Testing.Utils
{
    public class QuadraticFunctionTests : TestFoundation
    {
        [Test]
        public void ComputeTests()
        {
            QuadraticFunction func = new QuadraticFunction(2, 8, 6);

            Assert.AreEqual(96, func.Compute(5));
        }
    }
}
