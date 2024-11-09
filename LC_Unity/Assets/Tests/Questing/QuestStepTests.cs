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
    }
}
