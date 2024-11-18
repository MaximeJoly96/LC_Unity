using Menus.SubMenus.Quests;
using NUnit.Framework;
using Questing;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections.Generic;
using Inventory;
using Language;

namespace Testing.Menus.SubMenus.Quests
{
    public class QuestDetailsDisplayTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator DetailsCanBeCleared()
        {
            QuestDetailsDisplay display = ComponentCreator.CreateQuestDetailsDisplay();
            _usedGameObjects.Add(display.gameObject);

            yield return null;

            display.Clear();

            yield return null;

            Assert.AreEqual(string.Empty, display.NameLabel.text);
            Assert.AreEqual(string.Empty, display.DescriptionLabel.text);
            Assert.IsNull(display.QuestData);

            Assert.AreEqual(0, display.StepsDisplay.Steps.Count);
            Assert.AreEqual(0, display.StepsDisplay.transform.childCount);

            Assert.AreEqual(0, display.RewardsDisplay.RewardComponents.Count);
            Assert.AreEqual(0, display.RewardsDisplay.transform.childCount);
        }

        [UnityTest]
        public IEnumerator DetailsCanBeShown()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            ItemsWrapper wrapper = ComponentCreator.CreateItemsWrapper("Menus/SubMenus/Quests/TestData/Consumables.xml");
            _usedGameObjects.Add(wrapper.gameObject);

            yield return null;

            QuestDetailsDisplay display = ComponentCreator.CreateQuestDetailsDisplay();
            _usedGameObjects.Add(display.gameObject);

            InventoryItem itemReward = new InventoryItem(0, 5);
            QuestReward reward = new QuestReward(500, 1500, new List<InventoryItem> { itemReward });
            Quest quest = new Quest(0, "myQuest", "myQuestDescription", QuestType.Bounty, reward);
            quest.AddStep(new QuestStep(0, "myStep", "myStepDescription", new QuestReward()));
            quest.AddStep(new QuestStep(1, "myOtherStep", "myOtherStepDescription", new QuestReward()));

            display.RewardsDisplay.SetRewardComponentPrefab(ComponentCreator.CreateRewardComponentDisplay());
            display.StepsDisplay.SetStepDisplayPrefab(ComponentCreator.CreateQuestStepDisplay());

            yield return null;

            display.ShowQuestDetails(quest);

            yield return null;

            Assert.AreEqual(quest, display.QuestData);
            Assert.AreEqual("Ma quête", display.NameLabel.text);
            Assert.AreEqual("Description de ma quête", display.DescriptionLabel.text);

            Assert.AreEqual(2, display.StepsDisplay.Steps.Count);
            Assert.AreEqual("Etape de quête", display.StepsDisplay.Steps[0].StepLabel.text);
            Assert.AreEqual("Description de l'étape de quête", display.StepsDisplay.Steps[0].StepDescription.text);
            Assert.AreEqual(0, display.StepsDisplay.Steps[0].RewardDisplay.RewardComponents.Count);
            Assert.AreEqual(0, display.StepsDisplay.Steps[0].RewardDisplay.transform.childCount);
            Assert.AreEqual("L'autre étape de quête", display.StepsDisplay.Steps[1].StepLabel.text);
            Assert.AreEqual("Description de l'autre étape de quête", display.StepsDisplay.Steps[1].StepDescription.text);
            Assert.AreEqual(0, display.StepsDisplay.Steps[1].RewardDisplay.RewardComponents.Count);
            Assert.AreEqual(0, display.StepsDisplay.Steps[1].RewardDisplay.transform.childCount);

            Assert.AreEqual(3, display.RewardsDisplay.RewardComponents.Count);
            Assert.AreEqual("500", display.RewardsDisplay.RewardComponents[0].Quantity.text);
            Assert.AreEqual("XP", display.RewardsDisplay.RewardComponents[0].Label.text);
            Assert.AreEqual("1500", display.RewardsDisplay.RewardComponents[1].Quantity.text);
            Assert.AreEqual("Okanes", display.RewardsDisplay.RewardComponents[1].Label.text);
            Assert.AreEqual("5", display.RewardsDisplay.RewardComponents[2].Quantity.text);
            Assert.AreEqual("Potion", display.RewardsDisplay.RewardComponents[2].Label.text);
        }
    }
}
