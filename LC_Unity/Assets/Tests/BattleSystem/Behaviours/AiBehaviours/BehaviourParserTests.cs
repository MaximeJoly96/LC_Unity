using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class BehaviourParserTests
    {
        private readonly string _filePath = "Assets/Tests/BattleSystem/Behaviours/AiBehaviours/RangeAndResourcesTestBehaviour.xml";

        [Test]
        public void ParseMainConditionTest()
        {
            BehaviourParser parser = new BehaviourParser();
            BehaviourCondition condition = parser.ParseMainCondition(LoadBehaviourNode().ChildNodes[0]);

            Assert.IsNotNull(condition);
        }

        [Test]
        public void ParseIsInRangeConditionTest()
        {
            BehaviourParser parser = new BehaviourParser();
            BehaviourCondition condition = parser.ParseMainCondition(LoadBehaviourNode().ChildNodes[0]);

            Assert.IsTrue(condition is IsInRange);

            IsInRange isInRange = condition as IsInRange;

            Assert.AreEqual(1, isInRange.MinTargetCount);
            Assert.AreEqual(0, isInRange.MaxTargetCount);
            Assert.AreEqual(300, isInRange.Range);
        }

        [Test]
        public void ParseHasEnoughResourcesConditionTest()
        {
            BehaviourParser parser = new BehaviourParser();
            BehaviourCondition condition = parser.ParseMainCondition(LoadBehaviourNode().ChildNodes[0]);

            Assert.IsTrue(condition.ActionWhenFalse.Condition is HasEnoughResources);

            HasEnoughResources hasEnough = condition.ActionWhenFalse.Condition as HasEnoughResources;

            Assert.AreEqual(HasEnoughResources.AmountType.FromAbility, hasEnough.Amount);
            Assert.IsTrue(Mathf.Abs(45.0f - hasEnough.Value) < 0.01f);
            Assert.AreEqual(Effects.Stat.MP, hasEnough.Resource);
        }

        [Test]
        public void ParseTestBehaviourTest()
        {
            BehaviourParser parser = new BehaviourParser();
            AiScript script = parser.ParseBehaviour(AssetDatabase.LoadAssetAtPath<TextAsset>(_filePath));

            Assert.NotNull(script);
            Assert.NotNull(script.MainCondition);
            Assert.IsTrue(script.MainCondition is IsInRange);

            IsInRange isInRange = script.MainCondition as IsInRange;

            Assert.AreEqual(1, isInRange.MinTargetCount);
            Assert.AreEqual(0, isInRange.MaxTargetCount);
            Assert.AreEqual(300, isInRange.Range);

            Assert.AreEqual(1, isInRange.ActionWhenTrue.Abilities.Count);
            Assert.AreEqual(2, isInRange.ActionWhenTrue.Abilities[0]);
            Assert.IsTrue(isInRange.ActionWhenFalse.Condition is HasEnoughResources);

            HasEnoughResources hasEnough = isInRange.ActionWhenFalse.Condition as HasEnoughResources;

            Assert.AreEqual(HasEnoughResources.AmountType.FromAbility, hasEnough.Amount);
            Assert.IsTrue(Mathf.Abs(45.0f - hasEnough.Value) < 0.01f);
            Assert.AreEqual(Effects.Stat.MP, hasEnough.Resource);

            Assert.AreEqual(1, hasEnough.ActionWhenTrue.Abilities.Count);
            Assert.AreEqual(45, hasEnough.ActionWhenTrue.Abilities[0]);
            Assert.AreEqual(1, hasEnough.ActionWhenFalse.Abilities.Count);
            Assert.AreEqual(2, hasEnough.ActionWhenFalse.Abilities[0]);
        }

        private XmlNode LoadBehaviourNode()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>(_filePath).text);

            BehaviourParser parser = new BehaviourParser();

            return document.SelectSingleNode("Behaviour");
        }
    }
}
