using NUnit.Framework;
using Actors;
using UnityEngine;
using System;

namespace Testing.Actors
{
    public class ElementalAffinityTests
    {
        [Test]
        public void ElementalAffinityCanBeCreated()
        {
            ElementalAffinity fireAffinity = new ElementalAffinity(Element.Fire, 1.0f);
            ElementalAffinity iceAffinity = new ElementalAffinity(Element.Ice, 2.0f);

            Assert.AreEqual(Element.Fire, fireAffinity.Element);
            Assert.AreEqual(Element.Ice, iceAffinity.Element);
            Assert.IsTrue(Mathf.Abs(1.0f - fireAffinity.Multiplier) < 0.01f);
            Assert.IsTrue(Mathf.Abs(2.0f - iceAffinity.Multiplier) < 0.01f);
            Assert.Throws<ArgumentException>(delegate { new ElementalAffinity(Element.Darkness, -2.0f); });
        }
    }
}
