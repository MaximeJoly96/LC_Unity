using NUnit.Framework;
using Core.Model;
using Actors;
using Essence;

namespace Testing.Actors
{
    public class EssenceAffinityTests : TestFoundation
    {
        [Test]
        public void EssenceAffinityCanBeCreated()
        {
            EssenceAffinity mightAffinity = new EssenceAffinity(new ElementIdentifier(0, "might", "mightDesc"),
                                                                EssenceType.Might, new DamageTaken());
            EssenceAffinity hopeAffinity = new EssenceAffinity(new ElementIdentifier(1, "hope", "hopeDesc"),
                                                               EssenceType.Hope, new HealsOtherAlly());

            Assert.AreEqual(0, mightAffinity.Id);
            Assert.AreEqual("might", mightAffinity.Name);
            Assert.AreEqual("mightDesc", mightAffinity.Description);
            Assert.AreEqual(EssenceType.Might, mightAffinity.Essence);
            Assert.IsTrue(mightAffinity.Effect is DamageTaken);

            Assert.AreEqual(1, hopeAffinity.Id);
            Assert.AreEqual("hope", hopeAffinity.Name);
            Assert.AreEqual("hopeDesc", hopeAffinity.Description);
            Assert.AreEqual(EssenceType.Hope, hopeAffinity.Essence);
            Assert.IsTrue(hopeAffinity.Effect is HealsOtherAlly);
        }
    }
}
