using Questing;
using NUnit.Framework;
using System.Collections.Generic;
using Inventory;

namespace Testing.Questing
{
    public class QuestTests
    {
        [Test]
        public void EmptyQuestCanBeCreated()
        {
            Quest quest = new Quest(2, "myQuest", "myDescription", QuestType.Bounty, new QuestReward(20, 10, new List<InventoryItem>()));

            Assert.AreEqual(2, quest.Id);
            Assert.AreEqual("myQuest", quest.NameKey);
            Assert.AreEqual("myDescription", quest.DescriptionKey);
            Assert.AreEqual(QuestType.Bounty, quest.Type);
            Assert.AreEqual(20, quest.Reward.Exp);
            Assert.AreEqual(10, quest.Reward.Gold);
            Assert.AreEqual(0, quest.Reward.Items.Count);
            Assert.AreEqual(0, quest.Steps.Count);
            Assert.AreEqual(QuestStatus.NotRunning, quest.Status);
        }

        [Test]
        public void QuestStatusCanBeChanged()
        {
            Quest quest = new Quest(2, "myQuest", "myDescription", QuestType.Bounty, new QuestReward(20, 10, new List<InventoryItem>()));

            Assert.AreEqual(QuestStatus.NotRunning, quest.Status);

            quest.ChangeStatus(QuestStatus.Running);

            Assert.AreEqual(QuestStatus.Running, quest.Status);

            quest.ChangeStatus(QuestStatus.Failed);

            Assert.AreEqual(QuestStatus.Failed, quest.Status);

            quest.ChangeStatus(QuestStatus.Completed);

            Assert.AreEqual(QuestStatus.Completed, quest.Status);
        }

        [Test]
        public void QuestStepCanBeAdded()
        {
            Quest quest = new Quest(2, "myQuest", "myDescription", QuestType.Bounty, new QuestReward(20, 10, new List<InventoryItem>()));

            Assert.AreEqual(0, quest.Steps.Count);

            quest.AddStep(new QuestStep(1, "stepKey", "stepDescription", new QuestReward(0, 0, new List<InventoryItem>())));

            Assert.AreEqual(1, quest.Steps.Count);
            Assert.AreEqual(1, quest.Steps[0].Id);
            Assert.AreEqual("stepKey", quest.Steps[0].NameKey);
            Assert.AreEqual("stepDescription", quest.Steps[0].DescriptionKey);
        }

        [Test]
        public void QuestCanBeSerialized()
        {
            Quest quest = new Quest(2, "myQuest", "myDescription", QuestType.Bounty, new QuestReward(20, 10, new List<InventoryItem>()));

            string serialized = quest.Serialize();

            Assert.AreEqual("quest2;NotRunning", serialized);

            quest.ChangeStatus(QuestStatus.Running);

            serialized = quest.Serialize();

            Assert.AreEqual("quest2;Running", serialized);

            quest.ChangeStatus(QuestStatus.Failed);

            serialized = quest.Serialize();

            Assert.AreEqual("quest2;Failed", serialized);

            quest.ChangeStatus(QuestStatus.Completed);

            serialized = quest.Serialize();

            Assert.AreEqual("quest2;Completed", serialized);
        }

        [Test]
        public void QuestCanBeDeserialized()
        {
            string serializedCompleted = "quest2;Completed";
            string serializedFailed = "quest1;Failed";
            string serializedNotRunning = "quest3;NotRunning";
            string serializedRunning = "quest0;Running";

            Quest completedQuest = Quest.Deserialize(serializedCompleted);
            Quest failedQuest = Quest.Deserialize(serializedFailed);
            Quest notRunningQuest = Quest.Deserialize(serializedNotRunning);
            Quest runningQuest = Quest.Deserialize(serializedRunning);

            Assert.AreEqual(2, completedQuest.Id);
            Assert.AreEqual(QuestStatus.Completed, completedQuest.Status);
            Assert.AreEqual(1, failedQuest.Id);
            Assert.AreEqual(QuestStatus.Failed, failedQuest.Status);
            Assert.AreEqual(3, notRunningQuest.Id);
            Assert.AreEqual(QuestStatus.NotRunning, notRunningQuest.Status);
            Assert.AreEqual(0, runningQuest.Id);
            Assert.AreEqual(QuestStatus.Running, runningQuest.Status);
        }
    }
}
