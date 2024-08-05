using UnityEngine;
using MsgBox;
using Language;
using Core;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuExitGame : SystemSubMenuItem
    {
        public override void Select()
        {
            MessageBoxService.Instance.MessageBoxClosedWithResult.AddListener(ConfirmSelect);
            MessageBoxService.Instance.ShowYesNoMessage(Localizer.Instance.GetString("exitGameConfirmation"), MessageBoxType.Warning);
        }

        private void ConfirmSelect(MessageBoxAnswer result)
        {
            if (result == MessageBoxAnswer.Yes)
                Application.Quit();

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
        }
    }
}
