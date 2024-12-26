using Abilities;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Testing.Abilities
{
    public class AbilityParserTests : TestFoundation
    {
        private readonly string _filePath = "Assets/Tests/Abilities/TestAbilities.xml";

        [Test]
        public void AllAbilitiesCanBeLoadedFromFileTest()
        {
            List<Ability> abilities = AbilityParser.ParseAllAbilities(AssetDatabase.LoadAssetAtPath<TextAsset>(_filePath));

            Assert.AreEqual(3, abilities.Count);
            AssertIndividualAbility(abilities[0], 0, "AttackCommand", "AttackCommandDescription", new AbilityCost(0, 0, 0),
                                    0, -1, TargetEligibility.Any, AbilityUsability.BattleOnly, AbilityCategory.AttackCommand,
                                    "Channel", 0, "Strike", 1, 2);
            AssertIndividualAbility(abilities[1], 1, "Flee", "FleeCommandDescription", new AbilityCost(0, 0, 0),
                                    10, 0, TargetEligibility.Self, AbilityUsability.BattleOnly, AbilityCategory.FleeCommand,
                                    "", -1, "", -1, -1);
            AssertIndividualAbility(abilities[2], 2, "Claws", "ClawsDescription", new AbilityCost(0, 5, 5),
                                    0, 100, TargetEligibility.Any, AbilityUsability.BattleOnly, AbilityCategory.Skill,
                                    "Channel", 0, "Strike", 1, 2);
        }

        private void AssertIndividualAbility(Ability ability, int id, string name, string description, AbilityCost cost,
                                             int priority, int range, TargetEligibility eligibility, AbilityUsability usability,
                                             AbilityCategory category, string channelAnimationName, int channelParticlesId,
                                             string strikeAnimationName, int impactParticlesId, int projectileId)
        {
            Assert.AreEqual(id, ability.Id);
            Assert.AreEqual(name, ability.Name);
            Assert.AreEqual(description, ability.Description);
            Assert.AreEqual(cost.HealthCost, ability.Cost.HealthCost);
            Assert.AreEqual(cost.ManaCost, ability.Cost.ManaCost);
            Assert.AreEqual(cost.EssenceCost, ability.Cost.EssenceCost);
            Assert.AreEqual(priority, ability.Priority);
            Assert.AreEqual(range, ability.Range);
            Assert.AreEqual(eligibility, ability.TargetEligibility);
            Assert.AreEqual(usability, ability.Usability);
            Assert.AreEqual(category, ability.Category);
            Assert.AreEqual(channelAnimationName, ability.Animation.BattlerChannelAnimationName);
            Assert.AreEqual(channelParticlesId, ability.Animation.BattlerChannelAnimationParticlesId);
            Assert.AreEqual(strikeAnimationName, ability.Animation.BattlerStrikeAnimationName);
            Assert.AreEqual(impactParticlesId, ability.Animation.ImpactAnimationParticlesId);
            // FIXME Assert.AreEqual(projectileId, ability.Animation.Projectile.Id);
        }
    }
}
