using NUnit.Framework;
using Actors;

namespace Testing.Actors
{
    public class ActiveEffectTests : TestFoundation
    {
        [Test]
        public void ActiveEffectCanBeCreated()
        {
            ActiveEffect effect = new ActiveEffect { Effect = EffectType.Poison };

            Assert.AreEqual(effect.Effect, EffectType.Poison);
        }
    }
}
