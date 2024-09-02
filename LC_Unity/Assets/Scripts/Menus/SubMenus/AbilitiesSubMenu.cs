using Core;
using Inputs;
using Utils;

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

        protected override void HandleInputs(InputAction input)
        {
            if(!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuAbilitiesTab)
            {
                switch(input)
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
