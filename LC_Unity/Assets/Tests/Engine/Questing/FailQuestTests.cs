using Questing;
using Engine.Questing;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using Inventory;
using UnityEditor;

namespace Testing.Engine.Questing
{
    public class FailQuestTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

        [Test]
        public void FailQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            StartQuest start = new StartQuest
            {
                Id = 1
            };

            start.Run();

            FailQuest fail = new FailQuest
            {
                Id = 1
            };

            fail.Run();

            Assert.IsTrue(QuestManager.Instance.FailedQuests.Count(q => q.Id == fail.Id) == 1);
            Assert.AreEqual(QuestStatus.Failed, QuestManager.Instance.GetQuest(fail.Id).Status);
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
