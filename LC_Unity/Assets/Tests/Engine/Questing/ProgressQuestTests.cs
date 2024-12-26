using Questing;
using Engine.Questing;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEditor;

namespace Testing.Engine.Questing
{
    public class ProgressQuestTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

        [Test]
        public void ProgressQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            StartQuest start = new StartQuest
            {
                Id = 2
            };

            start.Run();

            ProgressQuest progress = new ProgressQuest
            {
                Id = 2,
                StepId = 0,
                StepStatus = QuestStepStatus.Completed
            };

            progress.Run();

            Quest quest = QuestManager.Instance.GetQuest(progress.Id);

            Assert.AreEqual(QuestStepStatus.Completed, quest.Steps.FirstOrDefault(s => s.Id == progress.StepId).Status);
        }

        private QuestsWrapper CreateQuestsWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            QuestsWrapper wrapper = go.AddComponent<QuestsWrapper>();
            wrapper.Feed(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestQuests.xml"));

            return wrapper;
        }

        private ItemsWrapper CreateEmptyWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);
            return go.AddComponent<ItemsWrapper>();
        }
    }
}
