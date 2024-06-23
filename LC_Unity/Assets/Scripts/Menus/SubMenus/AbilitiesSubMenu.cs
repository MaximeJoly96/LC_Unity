using Core;

namespace Menus.SubMenus
{
    public class AbilitiesSubMenu : SubMenu 
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuAbilitiesTab);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
        }
    }
}
