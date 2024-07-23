using Core;
using Save;

namespace Menus.SubMenus
{
    public class SystemSubMenu : SubMenu 
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
            SaveManager.Instance.CreateSaveFile(0);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }
    }
}
