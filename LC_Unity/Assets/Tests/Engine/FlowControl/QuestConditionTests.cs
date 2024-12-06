using NUnit.Framework;
using Questing;
using Engine.FlowControl;
using Engine.Events;
using Engine.GameProgression;
using GameProgression;
using Engine.Questing;
using System.Collections;
using UnityEngine.TestTools;

namespace Testing.Engine.FlowControl
{
    public class QuestConditionTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            PersistentDataHolder.Instance.Reset();

            QuestManager.Instance.Reset();

            QuestsWrapper questsWrapper = ComponentCreator.CreateQuestsWrapper("Engine/FlowControl/MainQuests.xml",
                                                                               "Engine/FlowControl/SideQuests.xml",
                                                                               "Engine/FlowControl/Bounties.xml",
                                                                               "Engine/FlowControl/ProfessionQuests.xml");

            _usedGameObjects.Add(questsWrapper.gameObject);

            EventsRunner runner = ComponentCreator.CreateEventsRunner();

            _usedGameObjects.Add(runner.gameObject);
        }

        [UnityTest]
        public IEnumerator QuestCompletedConditionCanBeExecuted()
        {
            QuestManager.Instance.StartQuest(new StartQuest { Id = 1 });

            QuestCompletedCondition condition = new QuestCompletedCondition
            {
                QuestId = 1,
                SequenceWhenTrue = CreateSequenceWithPositiveSwitch(),
                SequenceWhenFalse = CreateSequenceWithNegativeSwitch()
            };

            condition.Run();

            yield return null;

            Assert.IsFalse((bool)PersistentDataHolder.Instance.GetData("testSwitch"));

            QuestManager.Instance.CompleteQuest(new CompleteQuest { Id = 1 });
            condition.Run();

            yield return null;

            Assert.IsTrue((bool)PersistentDataHolder.Instance.GetData("testSwitch"));
        }

        [UnityTest]
        public IEnumerator QuestFailedConditionCanBeExecuted()
        {
            QuestManager.Instance.StartQuest(new StartQuest { Id = 2 });

            QuestFailedCondition condition = new QuestFailedCondition
            {
                QuestId = 2,
                SequenceWhenTrue = CreateSequenceWithPositiveSwitch(),
                SequenceWhenFalse = CreateSequenceWithNegativeSwitch()
            };

            condition.Run();

            yield return null;

            Assert.IsFalse((bool)PersistentDataHolder.Instance.GetData("testSwitch"));

            QuestManager.Instance.FailQuest(new FailQuest { Id = 2 });

            condition.Run();

            yield return null;

            Assert.IsTrue((bool)PersistentDataHolder.Instance.GetData("testSwitch"));
        }

        [UnityTest]
        public IEnumerator QuestStepCompletedConditionCanBeExecuted()
        {
            QuestManager.Instance.StartQuest(new StartQuest { Id = 3 });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 3, StepId = 0});

            QuestStepCompletedCondition condition = new QuestStepCompletedCondition
            {
                QuestId = 3,
                QuestStepId = 1,
                SequenceWhenTrue = CreateSequenceWithPositiveSwitch(),
                SequenceWhenFalse = CreateSequenceWithNegativeSwitch()
            };

            condition.Run();

            yield return null;

            Assert.IsFalse((bool)PersistentDataHolder.Instance.GetData("testSwitch"));

            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 3, StepId = 1, StepStatus = QuestStepStatus.Completed });

            condition.Run();

            yield return null;

            Assert.IsTrue((bool)PersistentDataHolder.Instance.GetData("testSwitch"));
        }

        [UnityTest]
        public IEnumerator QuestStartedConditionCanBeExecuted()
        {
            QuestStartedCondition condition = new QuestStartedCondition
            {
                QuestId = 3,
                SequenceWhenTrue = CreateSequenceWithPositiveSwitch(),
                SequenceWhenFalse = CreateSequenceWithNegativeSwitch()
            };

            condition.Run();

            yield return null;

            Assert.IsFalse((bool)PersistentDataHolder.Instance.GetData("testSwitch"));

            QuestManager.Instance.StartQuest(new StartQuest { Id = 3 });

            condition.Run();

            yield return null;

            Assert.IsTrue((bool)PersistentDataHolder.Instance.GetData("testSwitch"));
        }

        private EventsSequence CreateSequenceWithNegativeSwitch()
        {
            ControlSwitch ctrlSwitch = new ControlSwitch
            {
                Key = "testSwitch",
                Value = false
            };

            EventsSequence sequence = new EventsSequence();
            sequence.Add(ctrlSwitch);

            return sequence;
        }

        private EventsSequence CreateSequenceWithPositiveSwitch()
        {
            ControlSwitch ctrlSwitch = new ControlSwitch
            {
                Key = "testSwitch",
                Value = true
            };

            EventsSequence sequence = new EventsSequence();
            sequence.Add(ctrlSwitch);

            return sequence;
        }
    }
}
