using UI;
using System.Collections.Generic;
using Questing;

namespace Menus.SubMenus.Quests
{
    public class SelectableQuestsList : SelectableList
    {
        public void FeedQuests(List<Quest> quests)
        {
            for(int i = 0; i < quests.Count; i++)
            {
                SelectableQuest quest = AddItem() as SelectableQuest;
                quest.Feed(quests[i]);
            }
        }
    }
}
