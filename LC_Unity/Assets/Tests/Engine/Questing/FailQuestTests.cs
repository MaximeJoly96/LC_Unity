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
    public class FailQuestTests
    {
        private List<GameObject> _usedGameObjects;

        [SetUp]
        public void Setup()
        {
            QuestManager.Instance.Reset();
        }

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

        [Test]
        public void FailQuestCanRun()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/TestConsumables.xml"));

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
            wrapper.Feed(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/MainQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/SideQuests.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/Bounties.xml"),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Questing/TestData/ProfessionQuests.xml"));

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
