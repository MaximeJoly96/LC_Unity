using UI;
using Core;

namespace Menus.SubMenus.Quests
{
    public class QuestsHorizontalMenu : HorizontalMenu
    {
        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuQuestsTab;
        }
    }
}
