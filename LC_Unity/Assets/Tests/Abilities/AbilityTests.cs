using NUnit.Framework;
using Abilities;
using Core.Model;
using Effects;
using System.Collections.Generic;

namespace Testing.Abilitiess
{
    public class AbilityTests
    {
        [Test]
        public void AbilityCanBeCreatedTest()
        {
            Ability ability = new Ability(new ElementIdentifier(4, "nameKey", "descriptionKey"), -2,
                                          AbilityUsability.Always, TargetEligibility.All, AbilityCategory.Skill, 350);

            Assert.AreEqual(4, ability.Id);
            Assert.AreEqual("nameKey", ability.Name);
            Assert.AreEqual("descriptionKey", ability.Description);
            Assert.AreEqual(-2, ability.Priority);
            Assert.AreEqual(AbilityUsability.Always, ability.Usability);
            Assert.AreEqual(TargetEligibility.All, ability.TargetEligibility);
            Assert.AreEqual(AbilityCategory.Skill, ability.Category);
            Assert.AreEqual(350, ability.Range);

            Assert.NotNull(ability.Effects);
            Assert.AreEqual(0, ability.Effects.Count);
        }

        [Test]
        public void AbilityCostCanBeSetTest()
        {
            Ability ability = new Ability(new ElementIdentifier(4, "nameKey", "descriptionKey"), -2,
                                          AbilityUsability.Always, TargetEligibility.All, AbilityCategory.Skill, 350);
            ability.SetCost(30, 0, 4);

            Assert.AreEqual(30, ability.Cost.HealthCost);
            Assert.AreEqual(0, ability.Cost.ManaCost);
            Assert.AreEqual(4, ability.Cost.EssenceCost);

            ability.SetCost(new AbilityCost(100, 43, 25));

            Assert.AreEqual(100, ability.Cost.HealthCost);
            Assert.AreEqual(43, ability.Cost.ManaCost);
            Assert.AreEqual(25, ability.Cost.EssenceCost);
        }

        [Test]
        public void AbilityAnimationCanBeSetTest()
        {
            Ability ability = new Ability(new ElementIdentifier(4, "nameKey", "descriptionKey"), -2,
                                          AbilityUsability.Always, TargetEligibility.All, AbilityCategory.Skill, 350);
            ability.SetAnimation("channel", "strike", 3, 5, 2);

            Assert.AreEqual("channel", ability.Animation.BattlerChannelAnimationName);
            Assert.AreEqual("strike", ability.Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(3, ability.Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual(5, ability.Animation.ImpactAnimationParticlesId);
            // projectile Id should be tested

            ability.SetAnimation(new AbilityAnimation("cc", "str", 4, 6, 3));

            Assert.AreEqual("cc", ability.Animation.BattlerChannelAnimationName);
            Assert.AreEqual("str", ability.Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(4, ability.Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual(6, ability.Animation.ImpactAnimationParticlesId);
            // projectile Id should be tested
        }

        [Test]
        public void AbilityEffectsCanBeSetTest()
        {
            Ability ability = new Ability(new ElementIdentifier(4, "nameKey", "descriptionKey"), -2,
                                          AbilityUsability.Always, TargetEligibility.All, AbilityCategory.Skill, 350);
            
            Assert.AreEqual(0, ability.Effects.Count);

            List<IEffect> effects = new List<IEffect>
            {
                new StatBoost(),
                new InflictStatus()
            };
            ability.SetEffects(effects);

            Assert.AreEqual(2, ability.Effects.Count);
            Assert.IsTrue(ability.Effects[0] is StatBoost);
            Assert.IsTrue(ability.Effects[1] is InflictStatus);
        }
    }
}
