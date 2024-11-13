using Questing;
using NUnit.Framework;
using Engine.Questing;
using UnityEngine;
using UnityEditor;
using Inventory;

namespace Testing.Questing
{
    public class QuestManagerTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();

            ItemsWrapper itemsWrapper = ComponentCreator.CreateItemsWrapper("Questing/TestData/TestConsumables.xml");
            QuestsWrapper questsWrapper = ComponentCreator.CreateQuestsWrapper("Questing/TestData/MainQuests.xml",
                                                                               "Questing/TestData/SideQuests.xml",
                                                                               "Questing/TestData/Bounties.xml",
                                                                               "Questing/TestData/ProfessionQuests.xml");

            _usedGameObjects.Add(itemsWrapper.gameObject);
            _usedGameObjects.Add(questsWrapper.gameObject);
        }

        [Test]
        public void QuestManagerDataCanBeSerialized()
        {
            QuestManager.Instance.StartQuest(new StartQuest { Id = 0 });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 0, StepId = 0, StepStatus = QuestStepStatus.Completed });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 0, StepId = 1, StepStatus = QuestStepStatus.Unlocked });

            QuestManager.Instance.StartQuest(new StartQuest { Id = 1 });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 1, StepId = 0, StepStatus = QuestStepStatus.Failed });
            QuestManager.Instance.FailQuest(new FailQuest { Id = 1 });

            QuestManager.Instance.StartQuest(new StartQuest { Id = 2 });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 2, StepId = 0, StepStatus = QuestStepStatus.Completed });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 2, StepId = 1, StepStatus = QuestStepStatus.Completed });
            QuestManager.Instance.ProgressQuest(new ProgressQuest { Id = 2, StepId = 2, StepStatus = QuestStepStatus.Completed });
            QuestManager.Instance.CompleteQuest(new CompleteQuest { Id = 2 });

            string serialized = QuestManager.Instance.Serialize();
            string[] serializedSplit = serialized.Split("\r\n");

            Assert.AreEqual("quest0;Running", serializedSplit[0]);
            Assert.AreEqual("quest0step0;Completed", serializedSplit[1]);
            Assert.AreEqual("quest0step1;Unlocked", serializedSplit[2]);
            Assert.AreEqual("quest0step2;Locked", serializedSplit[3]);
            Assert.AreEqual("quest1;Failed", serializedSplit[4]);
            Assert.AreEqual("quest1step0;Failed", serializedSplit[5]);
            Assert.AreEqual("quest1step1;Locked", serializedSplit[6]);
            Assert.AreEqual("quest1step2;Locked", serializedSplit[7]);
            Assert.AreEqual("quest2;Completed", serializedSplit[8]);
            Assert.AreEqual("quest2step0;Completed", serializedSplit[9]);
            Assert.AreEqual("quest2step1;Completed", serializedSplit[10]);
            Assert.AreEqual("quest2step2;Completed", serializedSplit[11]);
        }

        [Test]
        public void QuestManagerDataCanBeDeserialized()
        {
            TextAsset textFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/SerializedQuestManager.txt");
            QuestManager.Instance.Deserialize(textFile.text);

            Assert.AreEqual(QuestStatus.Running, QuestManager.Instance.GetQuest(0).Status);
            Assert.AreEqual(QuestStepStatus.Unlocked, QuestManager.Instance.GetQuest(0).GetStep(0).Status);
            Assert.AreEqual(QuestStatus.Failed, QuestManager.Instance.GetQuest(2).Status);
            Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(2).GetStep(0).Status);
            Assert.AreEqual(QuestStepStatus.Failed, QuestManager.Instance.GetQuest(2).GetStep(1).Status);
            Assert.AreEqual(QuestStatus.Completed, QuestManager.Instance.GetQuest(3).Status);
            Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(3).GetStep(0).Status);
            Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(3).GetStep(1).Status);
            Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(3).GetStep(2).Status);
        }
    }
}
