using NUnit.Framework;
using Essence;
using System.Collections.Generic;
using Actors;
using UnityEditor;
using UnityEngine;

namespace Testing.Essence
{
    public class EssentialAffinitiesParserTests
    {
        [Test]
        public void EssentialAffinitiesCanBeParsed()
        {
            List<EssenceAffinity> affinities = EssentialAffinitiesParser.GetEssentialAffinities(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Essence/Essences.xml"));

            Assert.AreEqual(16, affinities.Count);
            Assert.AreEqual("altruismAffinity", affinities[0].Name);
            Assert.AreEqual(EssenceType.Knowledge, affinities[2].Essence);
            Assert.AreEqual(3, affinities[3].Id);
            Assert.AreEqual("wrathAffinityDescription", affinities[7].Description);

            Assert.IsTrue(affinities[0].Effect is HealsOtherAlly);
            Assert.IsTrue(affinities[1].Effect is SuffersNegativeStatus);
            Assert.IsTrue(affinities[2].Effect is SpendsMp);
            Assert.IsTrue(affinities[3].Effect is DifferentConsecutiveMoves);
            Assert.IsTrue(affinities[4].Effect is EnemySlain);
            Assert.IsTrue(affinities[5].Effect is DamageDealtToEnemy);
            Assert.IsTrue(affinities[6].Effect is DamageTaken);
            Assert.IsTrue(affinities[7].Effect is OtherAllyTakesDamage);
            Assert.IsTrue(affinities[8].Effect is ExploitElementalWeakness);
            Assert.IsTrue(affinities[9].Effect is AllyGetsPositiveStatus);
            Assert.IsTrue(affinities[10].Effect is StartsTurnWithLowHealth);
            Assert.IsTrue(affinities[11].Effect is IncreasesEachTurn);
            Assert.IsTrue(affinities[12].Effect is ParryOrEvasion);
            Assert.IsTrue(affinities[13].Effect is ScalesWithMovement);
            Assert.IsTrue(affinities[14].Effect is OtherAllyRevived);
            Assert.IsTrue(affinities[15].Effect is CriticalStrikeLanded);
        }
    }
}
