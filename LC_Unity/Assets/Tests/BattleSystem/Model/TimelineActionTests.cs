using NUnit.Framework;
using BattleSystem.Model;
using UnityEngine;
using BattleSystem;
using System.Collections.Generic;
using Abilities;
using Utils;
using Actors;
using System.Linq;
using Core.Model;

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
            Ability ability = new Ability(new ElementIdentifier(0, "name", "desc"), 0,
                                          AbilityUsability.Always, TargetEligibility.Self, AbilityCategory.Skill, 0);
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

        [Test]
        public void ComputeActionStartPointTest()
        {
            BattlerBehaviour b1 = CreateBattlerBehaviour();
            BattlerBehaviour b2 = CreateBattlerBehaviour();
            BattlerBehaviour b3 = CreateBattlerBehaviour();

            b1.BattlerData.Character.GiveExp(10);
            b2.BattlerData.Character.GiveExp(300);
            b3.BattlerData.Character.GiveExp(1000);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                b1, b2, b3
            };

            int maxAgility = battlers.Max(b => b.BattlerData.Character.Stats.BaseAgility + b.BattlerData.Character.Stats.BonusAgility);
            Assert.IsTrue(Mathf.Abs(90.0f - TimelineAction.ComputeActionStartPoint(b1, maxAgility)) < 0.01f);
            Assert.IsTrue(Mathf.Abs(40.0f - TimelineAction.ComputeActionStartPoint(b2, maxAgility)) < 0.01f);
            Assert.IsTrue(Mathf.Abs(0.0f - TimelineAction.ComputeActionStartPoint(b3, maxAgility)) < 0.01f);
        }

        private BattlerBehaviour CreateBattlerBehaviour()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            BattlerBehaviour battler = go.AddComponent<BattlerBehaviour>();
            battler.Feed(new Battler(CreateDummyCharacter()));
            return battler;
        }

        private Character CreateDummyCharacter()
        {
            return new Character(new ElementIdentifier(0, "name", ""),
                                 new QuadraticFunction(10.0f, 10.0f, 10.0f),
                                 new StatScalingFunction(100.0f, 1.0f, 100.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f));
        }
    }
}
