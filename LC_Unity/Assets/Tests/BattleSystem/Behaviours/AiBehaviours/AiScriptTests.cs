using BattleSystem;
using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Utils;
using Actors;
using BattleSystem.Model;
using Abilities;
using System.Collections.Generic;
using Core.Model;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class AiScriptTests
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
        public void SetMainConditionTest()
        {
            AiScript script = new AiScript();

            Assert.IsNull(script.MainCondition);

            script.SetMainCondition(new DefaultCondition());

            Assert.IsNotNull(script.MainCondition);
        }

        [Test]
        public void AiShouldSelectCorrectAbilityWhenTargetInRangeTest()
        {
            Ability ability1 = new Ability(new ElementIdentifier(2, "Claws", "desc"), 0,
                                           AbilityUsability.BattleOnly, TargetEligibility.Enemy, AbilityCategory.Skill, 0);
            ability1.SetCost(0, 0, 0);
            Ability ability2 = new Ability(new ElementIdentifier(45, "Magicka", "desc"), 0,
                                           AbilityUsability.BattleOnly, TargetEligibility.Enemy, AbilityCategory.Skill, 0);
            ability2.SetCost(0, 5, 0);
            AbilitiesManager.Instance.AddAbility(ability1);
            AbilitiesManager.Instance.AddAbility(ability2);

            GameObject battler = new GameObject("Battler");
            _usedGameObjects.Add(battler);
            BattlerBehaviour battlerComponent = battler.AddComponent<BattlerBehaviour>();
            battlerComponent.Feed(new Battler(CreateDummyCharacter()));

            GameObject battler1 = CreateBattler("b1", new Vector2(-2.0f, 0.3f)); // should not be in range
            GameObject battler2 = CreateBattler("b2", new Vector2(0.3f, 0.3f)); // shoud not be in range
            GameObject battler3 = CreateBattler("b3", new Vector2(0.1f, 0.1f)); // should be in range

            BehaviourParser parser = new BehaviourParser();
            AiScript script = parser.ParseBehaviour(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/BattleSystem/Behaviours/AiBehaviours/RangeAndResourcesTestBehaviour.xml"));

            Assert.AreEqual(2, script.PickAction(battlerComponent));

            battler.transform.position = new Vector3(1.0f, 1.0f); // should not be in range anymore

            Assert.AreEqual(45, script.PickAction(battlerComponent));

            battlerComponent.BattlerData.Character.Stats.CurrentMana = 0; // should not be able to cast its range ability

            Assert.AreEqual(2, script.PickAction(battlerComponent));
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

        private GameObject CreateBattler(string name, Vector3 position)
        {
            GameObject battler = new GameObject(name);
            _usedGameObjects.Add(battler);
            battler.AddComponent<BattlerBehaviour>();
            battler.AddComponent<BoxCollider2D>();

            battler.transform.position = position;

            return battler;
        }
    }
}
