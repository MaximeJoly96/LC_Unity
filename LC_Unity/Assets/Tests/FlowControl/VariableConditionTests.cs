using Engine.Events;
using Engine.FlowControl;
using GameProgression;
using NUnit.Framework;
using System.Collections.Generic;
using Testing.Engine;
using UnityEngine;

namespace Testing.FlowControl
{
    public class VariableConditionTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/FlowControl/VariableConditionTests.xml"; } }

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

        [SetUp]
        public void Setup()
        {
            GameObject runner = new GameObject("Runner");
            _usedGameObjects.Add(runner);
            runner.AddComponent<EventsRunner>();

            PersistentDataHolder.Instance.Reset();
            PersistentDataHolder.Instance.StoreData("testValue", -1);
        }

        [Test]
        public void VariableAsFirstMemberEqualsConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 5);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 0)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 4);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberEqualsConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 5);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 1)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 7);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariablesAreEqualTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 4);
            PersistentDataHolder.Instance.StoreData("myVar2", 4);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 2)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar2", 5);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsFirstMemberIsGreaterThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 5);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 3)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 12);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberIsGreaterThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 12);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 4)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", -3);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableIsGreaterThanAnotherVariableTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 30);
            PersistentDataHolder.Instance.StoreData("myVar2", 25);

            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 5)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar1", 25);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsFirstMemberIsSmallerThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", -6);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 6)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 4);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberIsSmallerThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 25);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 7)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", -3);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableIsSmallerThanAnotherVariableTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 3);
            PersistentDataHolder.Instance.StoreData("myVar2", 14);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 8)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar1", 34);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsFirstMemberIsDifferentThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 2);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 9)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 1);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberIsDifferentThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 8);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 10)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 12);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableIsDifferentThanAnotherVariableTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 4);
            PersistentDataHolder.Instance.StoreData("myVar2", 5);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 11)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar2", 4);

            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsFirstMemberIsEqualOrGreaterThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 1900);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 12)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 1901);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 1899);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberIsEqualOrGreaterThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 4567);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 13)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 4568);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 2000);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableIsEqualOrGreaterThanAnotherVariableTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 24);
            PersistentDataHolder.Instance.StoreData("myVar2", 48);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 14)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar1", 50);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar2", 50);

            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsFirstMemberIsEqualOrSmallerThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 3);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 15)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 4);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 5);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableAsSecondMemberIsEqualOrSmallerThanConstantTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar", 89);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 16)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 80);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar", 90);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }

        [Test]
        public void VariableIsEqualOrSmallerThanAnotherVariableTest()
        {
            PersistentDataHolder.Instance.StoreData("myVar1", 40);
            PersistentDataHolder.Instance.StoreData("myVar2", 40);
            VariableCondition condition = XmlFlowControlParser.ParseConditionalBranch(GetDataToParse("ConditionalBranch", 17)) as VariableCondition;
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar1", 38);
            condition.Run();

            Assert.AreEqual(1, PersistentDataHolder.Instance.GetData("testValue"));

            PersistentDataHolder.Instance.StoreData("myVar2", 35);
            condition.Run();

            Assert.AreEqual(0, PersistentDataHolder.Instance.GetData("testValue"));
        }
    }
}
