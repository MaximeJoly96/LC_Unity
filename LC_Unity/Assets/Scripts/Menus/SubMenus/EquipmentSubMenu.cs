using Core;

namespace Menus.SubMenus
{
    public class EquipmentSubMenu : SubMenu
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuEquipmentTab);
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
