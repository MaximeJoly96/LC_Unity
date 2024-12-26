using NUnit.Framework;
using Questing;
using Engine.Questing;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;
using Engine.Events;
using Timing;
using UnityEditor;
using Inventory;
using GameProgression;

namespace Testing.Engine.Questing
{
    public class QuestRunTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator AQuestCanBeStartedAndProgressed()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestSequence1.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);
            bool finished = false;

            sequence.Finished.AddListener(() => finished = true);
            sequence.Events[0].Finished.AddListener(() => Assert.AreEqual(QuestStatus.Running, QuestManager.Instance.GetQuest(0).Status));
            sequence.Events[1].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Unlocked, QuestManager.Instance.GetQuest(0).GetStep(0).Status));
            sequence.Events[2].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(0).GetStep(0).Status));

            EventsRunner runner = Setup();
            runner.RunEvents(sequence);

            yield return new WaitUntil(() => finished);
        }

        [UnityTest]
        public IEnumerator AQuestCanBeStartedAndFailed()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestSequence2.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);
            bool finished = false;

            sequence.Finished.AddListener(() => finished = true);
            sequence.Events[0].Finished.AddListener(() => Assert.AreEqual(QuestStatus.Running, QuestManager.Instance.GetQuest(1).Status));
            sequence.Events[1].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(1).GetStep(0).Status));
            sequence.Events[2].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(1).GetStep(1).Status));
            sequence.Events[3].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Failed, QuestManager.Instance.GetQuest(1).GetStep(2).Status));
            sequence.Events[4].Finished.AddListener(() => Assert.AreEqual(QuestStatus.Failed, QuestManager.Instance.GetQuest(1).Status));

            EventsRunner runner = Setup();
            runner.RunEvents(sequence);

            yield return new WaitUntil(() => finished);
        }

        [UnityTest]
        public IEnumerator AQuestCanBeStartedAndCompleted()
        {
            CreateQuestsWrapper();

            ItemsWrapper wrapper = CreateEmptyWrapper();
            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestConsumables.xml"));

            TextAsset file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Questing/TestData/TestSequence3.xml");
            EventsSequence sequence = EventsSequenceParser.ParseEventsSequence(file);
            bool finished = false;

            sequence.Finished.AddListener(() => finished = true);
            sequence.Events[0].Finished.AddListener(() => Assert.AreEqual(QuestStatus.Running, QuestManager.Instance.GetQuest(2).Status));
            sequence.Events[1].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(2).GetStep(0).Status));
            sequence.Events[2].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(2).GetStep(1).Status));
            sequence.Events[3].Finished.AddListener(() => Assert.AreEqual(QuestStepStatus.Completed, QuestManager.Instance.GetQuest(2).GetStep(2).Status));
            sequence.Events[4].Finished.AddListener(() => Assert.AreEqual(QuestStatus.Completed, QuestManager.Instance.GetQuest(2).Status));

            EventsRunner runner = Setup();
            runner.RunEvents(sequence);

            yield return new WaitUntil(() => finished);
        }

        private EventsRunner Setup()
        {
            GameObject go = new GameObject("Runner");
            _usedGameObjects.Add(go);
            return go.AddComponent<EventsRunner>();
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
