using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Testing.Utils
{
    public class MeasuresConverterTests
    {
        [Test]
        public void WorldUnitsToRangeTest()
        {
            Assert.IsTrue(Mathf.Abs(500.0f - MeasuresConverter.WorldUnitsToRange(1.0f)) < 0.01f);
        }

        [Test]
        public void RangeToWorldUnitsTest()
        {
            Assert.IsTrue(Mathf.Abs(1.0f - MeasuresConverter.RangeToWorldUnits(500.0f)) < 0.01f);
        }
    }
}
