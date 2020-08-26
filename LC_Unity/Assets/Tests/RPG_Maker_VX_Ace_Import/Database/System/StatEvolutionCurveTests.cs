using RPG_Maker_VX_Ace_Import.Database.System;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class StatEvolutionCurveTests
    {
        [Test]
        public void CreateStatEvolutionCurveTest()
        {
            StatEvolutionCurve curve = new StatEvolutionCurve(2.0f, 1.0f, 0.0f);

            Assert.IsTrue(Mathf.Abs(2.0f - curve.SquareXCoeff) < 0.001f);
            Assert.IsTrue(Mathf.Abs(1.0f - curve.XCoeff) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.0f - curve.IndependantTerm) < 0.001f);
        }

        [Test]
        public void GetValueBasedOnLevelTest()
        {
            StatEvolutionCurve curve = new StatEvolutionCurve(2.0f, 1.0f, 0.0f);

            Assert.IsTrue(Mathf.Abs(curve.GetValueBasedOnLevel(2) - 10.0f) < 0.001f);
            Assert.IsFalse(Mathf.Abs(curve.GetValueBasedOnLevel(3) - 15.0f) < 0.001f); // Expected is 21
        }
    }
}

