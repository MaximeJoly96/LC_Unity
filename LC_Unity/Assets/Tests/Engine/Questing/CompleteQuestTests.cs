using Questing;
using Engine.Questing;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using Inventory;
using UnityEditor;

namespace Testing.Engine.Questing
{
    public class CompleteQuestTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

        [Test]
        public void CompleteQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            StartQuest start = new StartQuest
            {
                Id = 0
            };

            start.Run();

            CompleteQuest complete = new CompleteQuest
            {
                Id = 0
            };

            complete.Run();

            Assert.IsTrue(QuestManager.Instance.CompletedQuests.Count(q => q.Id == complete.Id) == 1);
            Assert.AreEqual(QuestStatus.Completed, QuestManager.Instance.GetQuest(complete.Id).Status);
        }

        private QuestsWrapper CreateQuestsWrapper()
        {
            GameObject go = new GameObject();
            _usedGameObjects.Add(go);

            QuestsWrapper wrapper = go.AddComponent<QuestsWrapper>();
            wrapper.Feed(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestQuests.xml"));

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
