using Questing;
using UI;
using NUnit.Framework;
using TMPro;
using Menus.SubMenus.Quests;
using Language;

namespace Testing.Menus.SubMenus.Quests
{
    public class SelectableQuestTests : TestFoundation
    {
        [Test]
        public void QuestDataCanBeFed()
        {
            Localizer localizer = ComponentCreator.CreateLocalizer("Menus/SubMenus/Quests/french.csv", global::Language.Language.French);
            _usedGameObjects.Add(localizer.gameObject);

            Quest quest = new Quest(3, "myQuest", "myQuestDescription", QuestType.Bounty, new QuestReward());

            SelectableQuest item = ComponentCreator.CreateSelectableQuest();
            _usedGameObjects.Add(item.gameObject);
            
            item.Feed(quest);

            Assert.AreEqual("Ma quête", item.Label.text);
        }
    }
}
