using NUnit.Framework;
using BattleSystem.Model;
using UnityEngine;
using BattleSystem;
using System.Collections.Generic;
using Abilities;

namespace Testing.BattleSystem.Model
{
    public class TimelineActionTests
    {
        private List<GameObject> _usedGameObjects;

        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < _usedGameObjects.Count; i++)
            {
                GameObject.Destroy(_usedGameObjects[i]);
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _usedGameObjects = new List<GameObject>();
        }

        [Test]
        public void TimelineActionCanBeCreatedFromValuesTest()
        {
            TimelineAction timeline = new TimelineAction(3.0f, 2.5f, 1);

            Assert.IsTrue(Mathf.Abs(3.0f - timeline.Length) < 0.01f);
            Assert.IsTrue(Mathf.Abs(2.5f - timeline.StartPoint) < 0.01f);
            Assert.AreEqual(1, timeline.Priority);
        }

        [Test]
        public void TimelineActionCanBeCreatedFromBattlerBehaviourTest()
        {
            BattlerBehaviour battler = CreateBattlerBehaviour();
            Ability ability = new Ability(0, "name", "desc", new AbilityCost(0, 0, 0), 
                                          AbilityUsability.Always, 0, TargetEligibility.Self, AbilityCategory.Skill);
            ability.Targets = new List<BattlerBehaviour>
            {
                battler
            };

            battler.LockedInAbility = ability;
            TimelineAction timeline = new TimelineAction(battler);

            Assert.IsTrue(Mathf.Abs(0.0f - timeline.Length) < 0.01f);
            Assert.IsTrue(Mathf.Abs(0.0f - timeline.StartPoint) < 0.01f);
            Assert.AreEqual(0, timeline.Priority);
        }

        private BattlerBehaviour CreateBattlerBehaviour()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            BattlerBehaviour battler = go.AddComponent<BattlerBehaviour>();
            return battler;
        }
    }
}
