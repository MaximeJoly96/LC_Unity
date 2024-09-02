using Core;
using Inputs;
using Utils;

namespace Menus.SubMenus
{
    public class QuestsSubMenu : SubMenu
    {
        public override void Open()
        {
            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuQuestsTab);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        protected override void HandleInputs(InputAction input)
        {
            if (!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuQuestsTab)
            {
                switch (input)
                {
                    case InputAction.Cancel:
                        CommonSounds.ActionCancelled();
                        Close();
                        break;
                }
            }
        }
    }
}
