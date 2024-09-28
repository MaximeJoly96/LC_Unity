using Engine.FlowControl;
using GameProgression;
using NUnit.Framework;
using Testing.Engine;

namespace Testing.FlowControl
{
    public class SwitchConditionTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/FlowControl/SwitchConditionTests.xml"; } }

        [Test]
        public void FirstMemberAsVariableEqualsConstantTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 0)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch", true);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void SecondMemberAsVariableEqualsConstantTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 1)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch", true);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void BothMembersAsVariableEqualsTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 2)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch1", true);
            PersistentDataHolder.Instance.StoreData("mySwitch2", true);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void FirstMemberAsVariableNotEqualToConstantTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 3)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch", true);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void SecondMemberAsVariableNotEqualToConstantTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 4)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch", true);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void BothMembersAsVariableAreNotEqualTest()
        {
            SwitchCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 5)) as SwitchCondition;
            PersistentDataHolder.Instance.StoreData("mySwitch1", true);
            PersistentDataHolder.Instance.StoreData("mySwitch2", false);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();
            PersistentDataHolder.Instance.StoreData("testValue", -1);
        }
    }
}
