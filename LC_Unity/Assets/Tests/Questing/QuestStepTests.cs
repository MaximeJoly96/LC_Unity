using Questing;
using NUnit.Framework;
using Inventory;
using System.Collections.Generic;

namespace Testing.Questing
{
    public class QuestStepTests
    {
        [Test]
        public void EmptyQuestStepCanBeCreated()
        {
            QuestStep step = new QuestStep(2, "myStep", "stepDesc", new QuestReward(3, 5, new List<InventoryItem>()));

            Assert.AreEqual(2, step.Id);
            Assert.AreEqual("myStep", step.NameKey);
            Assert.AreEqual("stepDesc", step.DescriptionKey);
            Assert.AreEqual(3, step.Reward.Exp);
            Assert.AreEqual(5, step.Reward.Gold);
            Assert.AreEqual(0, step.Reward.Items.Count);
            Assert.AreEqual(QuestStepStatus.Locked, step.Status);
        }

        [Test]
        public void QuestStepStatusCanBeChanged()
        {
            QuestStep step = new QuestStep(2, "myStep", "stepDesc", new QuestReward(3, 5, new List<InventoryItem>()));

            Assert.AreEqual(QuestStepStatus.Locked, step.Status);

            step.ChangeStatus(QuestStepStatus.Unlocked);

            Assert.AreEqual(QuestStepStatus.Unlocked, step.Status);

            step.ChangeStatus(QuestStepStatus.Failed);

            Assert.AreEqual(QuestStepStatus.Failed, step.Status);

            step.ChangeStatus(QuestStepStatus.Completed);

            Assert.AreEqual(QuestStepStatus.Completed, step.Status);
        }

        [Test]
        public void QuestStepCanBeSerialized()
        {
            QuestStep step = new QuestStep(2, "myStep", "stepDesc", new QuestReward(3, 5, new List<InventoryItem>()));

            string serialized = step.Serialize();

            Assert.AreEqual("step2;Locked", serialized);

            step.ChangeStatus(QuestStepStatus.Failed);

            serialized = step.Serialize();

            Assert.AreEqual("step2;Failed", serialized);

            step.ChangeStatus(QuestStepStatus.Completed);

            serialized = step.Serialize();

            Assert.AreEqual("step2;Completed", serialized);

            step.ChangeStatus(QuestStepStatus.Unlocked);

            serialized = step.Serialize();

            Assert.AreEqual("step2;Unlocked", serialized);
        }

        [Test]
        public void QuestStepCanBeDeserialized()
        {
            string serializedFailed = "step3;Failed";
            string serializedLocked = "step2;Locked";
            string serializedUnlocked = "step4;Unlocked";
            string serializedCompleted = "step1;Completed";

            QuestStep stepFailed = QuestStep.Deserialize(serializedFailed);
            QuestStep stepLocked = QuestStep.Deserialize(serializedLocked);
            QuestStep stepUnlocked = QuestStep.Deserialize(serializedUnlocked);
            QuestStep stepCompleted = QuestStep.Deserialize(serializedCompleted);

            Assert.AreEqual(3, stepFailed.Id);
            Assert.AreEqual(QuestStepStatus.Failed, stepFailed.Status);
            Assert.AreEqual(2, stepLocked.Id);
            Assert.AreEqual(QuestStepStatus.Locked, stepLocked.Status);
            Assert.AreEqual(4, stepUnlocked.Id);
            Assert.AreEqual(QuestStepStatus.Unlocked, stepUnlocked.Status);
            Assert.AreEqual(1, stepCompleted.Id);
            Assert.AreEqual(QuestStepStatus.Completed, stepCompleted.Status);
        }
    }
}
