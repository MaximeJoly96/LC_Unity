using Core;
using Save;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuSaveGame : SystemSubMenuItem
    {
        public override void Select()
        {
            SaveManager.Instance.SaveCancelledEvent.RemoveAllListeners();
            SaveManager.Instance.SaveCancelledEvent.AddListener(() => GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab));
            SaveManager.Instance.InitSaveCreation();
        }
    }
}
