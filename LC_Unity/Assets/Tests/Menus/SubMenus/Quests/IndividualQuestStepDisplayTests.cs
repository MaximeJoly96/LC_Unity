using NUnit.Framework;
using Questing;
using System.Collections;
using UnityEngine.TestTools;
using Menus.SubMenus.Quests;
using Language;
using UnityEngine;

namespace Testing.Menus.SubMenus.Quests
{
    public class IndividualQuestStepDisplayTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator StepCanBeInitialized()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            IndividualQuestStepDisplay stepDisplay = ComponentCreator.CreateQuestStepDisplay();
            _usedGameObjects.Add(stepDisplay.gameObject);

            QuestStep step = new QuestStep(0, "myStep", "myStepDescription", new QuestReward());

            yield return null;

            stepDisplay.Init(step);

            yield return null;

            Assert.AreEqual(step, stepDisplay.QuestStepData);
            Assert.AreEqual("Etape de quête", stepDisplay.StepLabel.text);
            Assert.AreEqual("Description de l'étape de quête", stepDisplay.StepDescription.text);
            Assert.AreEqual(0, stepDisplay.RewardDisplay.RewardComponents.Count);
            Assert.AreEqual(0, stepDisplay.RewardDisplay.transform.childCount);
        }

        [UnityTest]
        public IEnumerator VisualStatusCanBeUpdated()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            IndividualQuestStepDisplay stepDisplay = ComponentCreator.CreateQuestStepDisplay();
            _usedGameObjects.Add(stepDisplay.gameObject);

            QuestStep step = new QuestStep(0, "myStep", "myStepDescription", new QuestReward());
            step.ChangeStatus(QuestStepStatus.Unlocked);

            yield return null;

            stepDisplay.Init(step);

            yield return null;

            AssertVisualStatus(stepDisplay, Color.white);

            step.ChangeStatus(QuestStepStatus.Locked);
            stepDisplay.Init(step);

            yield return null;

            AssertVisualStatus(stepDisplay, Color.clear);

            step.ChangeStatus(QuestStepStatus.Completed);
            stepDisplay.Init(step);

            yield return null;

            AssertVisualStatus(stepDisplay, Color.green);

            step.ChangeStatus(QuestStepStatus.Failed);
            stepDisplay.Init(step);

            yield return null;

            AssertVisualStatus(stepDisplay, Color.red);
        }

        private void AssertVisualStatus(IndividualQuestStepDisplay stepDisplay, Color color)
        {
            Assert.IsTrue(Mathf.Abs(color.r - stepDisplay.StepLabel.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.g - stepDisplay.StepLabel.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.b - stepDisplay.StepLabel.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.a - stepDisplay.StepLabel.color.a) < 0.01f);

            Assert.IsTrue(Mathf.Abs(color.r - stepDisplay.StepDescription.color.r) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.g - stepDisplay.StepDescription.color.g) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.b - stepDisplay.StepDescription.color.b) < 0.01f);
            Assert.IsTrue(Mathf.Abs(color.a - stepDisplay.StepDescription.color.a) < 0.01f);
        }
    }
}
