using Core;
using Menus;

namespace Mobile
{
    public class MobileUiMenuButton : MobileUiButton
    {
        public override void Execute()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.OnField)
            {
                FindObjectOfType<MainMenuController>().Open();
            }
        }
    }
}
