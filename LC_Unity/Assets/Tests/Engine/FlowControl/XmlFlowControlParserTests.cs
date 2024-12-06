using System.Collections;
using System.Collections.Generic;
using Engine.FlowControl;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.Engine.FlowControl
{
    public class XmlFlowControlParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/FlowControl/TestData.xml"; } }

        [Test]
        public void ParseSwitchConditionTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 0)) as SwitchCondition;

            Assert.AreEqual(SwitchCondition.Type.NotEqual, condition.Condition);
            Assert.AreEqual("var1", condition.FirstMember);
            Assert.AreEqual("false", condition.SecondMember);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }

        [Test]
        public void ParseVariableConditionTest()
        {
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 1)) as VariableCondition;

            Assert.AreEqual(VariableCondition.Type.GreaterThan, condition.Condition);
            Assert.AreEqual("var1", condition.FirstMember);
            Assert.AreEqual("2", condition.SecondMember);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }

        [Test]
        public void ParseTimerConditionTest()
        {
            TimerCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 2)) as TimerCondition;

            Assert.AreEqual(TimerCondition.Type.Before, condition.Condition);
            Assert.AreEqual("var1", condition.FirstMember);
            Assert.AreEqual("30", condition.SecondMember);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }

        [Test]
        public void ParseItemPossessedConditionTest()
        {
            ItemPossessed possessed = XmlFlowControlParser.ParseInventoryCondition(GetDataToParse("InventoryCondition", 0)) as ItemPossessed;

            Assert.AreEqual(0, possessed.ItemId);
            Assert.AreEqual(1, possessed.MinQuantity);

            Assert.NotNull(possessed.SequenceWhenFalse);
            Assert.NotNull(possessed.SequenceWhenTrue);
        }

        [Test]
        public void ParseItemEquippedConditionTest()
        {
            ItemEquipped equipped = XmlFlowControlParser.ParseInventoryCondition(GetDataToParse("InventoryCondition", 1)) as ItemEquipped;

            Assert.AreEqual(0, equipped.ItemId);

            Assert.NotNull(equipped.SequenceWhenFalse);
            Assert.NotNull(equipped.SequenceWhenTrue);
        }

        [Test]
        public void QuestCompletedConditionCanBeParsed()
        {
            QuestCompletedCondition condition = XmlFlowControlParser.ParseQuestCondition(GetDataToParse("QuestCondition", 0)) as QuestCompletedCondition;

            Assert.AreEqual(1, condition.QuestId);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }

        [Test]
        public void QuestStepCompletedConditionCanBeParsed()
        {
            QuestStepCompletedCondition condition = XmlFlowControlParser.ParseQuestCondition(GetDataToParse("QuestCondition", 1)) as QuestStepCompletedCondition;

            Assert.AreEqual(2, condition.QuestId);
            Assert.AreEqual(1, condition.QuestStepId);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }

        [Test]
        public void QuestFailedConditionCanBeParsed()
        {
            QuestFailedCondition condition = XmlFlowControlParser.ParseQuestCondition(GetDataToParse("QuestCondition", 2)) as QuestFailedCondition;

            Assert.AreEqual(3, condition.QuestId);

            Assert.NotNull(condition.SequenceWhenFalse);
            Assert.NotNull(condition.SequenceWhenTrue);
        }
    }
}
