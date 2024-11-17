using UI;
using System.Collections.Generic;
using Questing;
using Core;

namespace Menus.SubMenus.Quests
{
    public class SelectableQuestsList : SelectableList
    {
        public Quest SelectedQuest
        {
            get
            {
                return (SelectedItem as SelectableQuest).QuestData;
            }
        }

        public void FeedQuests(List<Quest> quests)
        {
            Clear();

            for(int i = 0; i < quests.Count; i++)
            {
                SelectableQuest quest = AddItem() as SelectableQuest;
                quest.Feed(quests[i]);
            }
        }

        protected override bool CanReceiveInputs()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BrowsingQuests;
        }
    }
}
