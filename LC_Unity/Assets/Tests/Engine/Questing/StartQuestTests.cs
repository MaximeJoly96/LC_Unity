using Questing;
using Engine.Questing;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Inventory;

namespace Testing.Engine.Questing
{
    public class StartQuestTests : TestFoundation
    {
        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

        [Test]
        public void StartQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            StartQuest start = new StartQuest
            {
                Id = 2
            };

            start.Run();

            Assert.IsTrue(QuestManager.Instance.RunningQuests.Count(x => x.Id == start.Id) == 1);
            Assert.AreEqual(QuestStatus.Running, QuestManager.Instance.GetQuest(start.Id).Status);
            Assert.AreEqual("quest2", QuestManager.Instance.GetQuest(start.Id).NameKey);
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
