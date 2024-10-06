using BattleSystem;
using BattleSystem.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;
using Actors;
using BattleSystem.Model;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Core.Model;

namespace Testing.BattleSystem.Behaviours.UI
{
    public class TimelineUiControllerTests
    {
        private List<GameObject> _usedGameObjects;

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
        public void DataCanBeFedToControllerTest()
        {
            TimelineUiController controller = CreateController();
            GameObject charactersWrapper = new GameObject("Characters");
            _usedGameObjects.Add(charactersWrapper);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                CreateBattler(),
                CreateBattler(),
                CreateBattler()
            };

            controller.Feed(battlers, charactersWrapper.transform, GetDefaultBattlerTimeline());

            Assert.AreEqual(3, controller.BattlersCount);
            Assert.AreEqual(3, controller.TimelinesCount);
        }

        [UnityTest]
        public IEnumerator ShowTest()
        {
            TimelineUiController controller = CreateController();
            GameObject charactersWrapper = new GameObject("Characters");
            _usedGameObjects.Add(charactersWrapper);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                CreateBattler(),
                CreateBattler(),
                CreateBattler()
            };

            controller.Feed(battlers, charactersWrapper.transform, GetDefaultBattlerTimeline());
            controller.Show();

            yield return null;

            Assert.IsTrue(charactersWrapper.activeSelf);
        }

        [UnityTest]
        public IEnumerator FinishedOpeningTest()
        {
            TimelineUiController controller = CreateController();
            GameObject charactersWrapper = new GameObject("Characters");
            _usedGameObjects.Add(charactersWrapper);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                CreateBattler(),
                CreateBattler(),
                CreateBattler()
            };

            controller.Feed(battlers, charactersWrapper.transform, GetDefaultBattlerTimeline());
            controller.FinishedOpening();

            yield return null;
            Assert.IsTrue(charactersWrapper.activeSelf);
        }

        [Test]
        public void TimelineWithoutAbilityIsUpdatedCorrectlyTest()
        {
            TimelineUiController controller = CreateController();
            GameObject charactersWrapper = new GameObject("Characters");
            _usedGameObjects.Add(charactersWrapper);

            List<BattlerBehaviour> battlers = new List<BattlerBehaviour>
            {
                CreateBattler(),
                CreateBattler(),
                CreateBattler()
            };

            controller.Feed(battlers, charactersWrapper.transform, GetDefaultBattlerTimeline());
            for (int i = 0; i < controller.Timelines.Count; i++)
            {
                controller.Timelines[i].ComputeAction(battlers[i]);
            }

            controller.UpdateTimeline();

            for(int i = 0; i  < controller.Timelines.Count; i++)
            {
                Assert.AreEqual(battlers[i], controller.Timelines[i].Battler);
                Assert.IsTrue(Mathf.Abs(0.0f - controller.Timelines[i].Action.Length) < 0.01f);
                Assert.IsTrue(Mathf.Abs(0.0f - controller.Timelines[i].Action.StartPoint) < 0.01f);
                Assert.AreEqual(0, controller.Timelines[i].Action.Priority);
                Assert.IsFalse(controller.Timelines[i].Processed);
            }
        }

        private TimelineUiController CreateController()
        {
            GameObject timelineUiCtrlGo = new GameObject("TimelineUiController");
            _usedGameObjects.Add(timelineUiCtrlGo);
            return timelineUiCtrlGo.AddComponent<TimelineUiController>();
        }

        private BattlerBehaviour CreateBattler()
        {
            GameObject battler = new GameObject();
            _usedGameObjects.Add(battler);
            BattlerBehaviour behaviour = battler.AddComponent<BattlerBehaviour>();
            behaviour.Feed(new Battler(CreateDummyCharacter()));
            return behaviour;
        }

        private BattlerTimeline GetDefaultBattlerTimeline()
        {
            GameObject timeline = new GameObject();
            _usedGameObjects.Add(timeline);
            BattlerTimeline battlerTimeline = timeline.AddComponent<BattlerTimeline>();
            battlerTimeline.SetText(timeline.AddComponent<TextMeshProUGUI>());
            battlerTimeline.SetTimelineImage(CreateTimelineImage());
            return battlerTimeline;
        }

        private Image CreateTimelineImage()
        {
            GameObject imgGo = new GameObject();
            _usedGameObjects.Add(imgGo);
            Image img = imgGo.AddComponent<Image>();
            img.fillMethod = Image.FillMethod.Horizontal;

            return img;
        }

        private Character CreateDummyCharacter()
        {
            return new Character(new ElementIdentifier(0, "name", ""),
                                 new QuadraticFunction(10.0f, 10.0f, 10.0f),
                                 new StatScalingFunction(100.0f, 1.0f, 100.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f));
        }
    }
}
