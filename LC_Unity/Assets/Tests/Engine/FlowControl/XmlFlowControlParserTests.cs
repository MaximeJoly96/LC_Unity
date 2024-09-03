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
    }
}
