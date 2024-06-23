using Core;

namespace Menus.SubMenus
{
    public class ItemsSubMenu : SubMenu
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuItemsTab);
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
