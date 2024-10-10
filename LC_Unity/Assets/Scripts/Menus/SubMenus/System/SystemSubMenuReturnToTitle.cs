using Core;
using Language;
using MsgBox;
using MusicAndSounds;
using UnityEngine.SceneManagement;

namespace Menus.SubMenus.System
{
    public class SystemSubMenuReturnToTitle : SystemSubMenuItem
    {
        public override void Select()
        {
            MessageBoxService.Instance.MessageBoxClosedWithResult.RemoveAllListeners();
            MessageBoxService.Instance.MessageBoxClosedWithResult.AddListener(ConfirmSelect);
            MessageBoxService.Instance.ShowYesNoMessage(Localizer.Instance.GetString("returnToTitleConfirmation"), MessageBoxType.Warning);
        }

        private void ConfirmSelect(MessageBoxAnswer result)
        {
            if (result == MessageBoxAnswer.Yes)
            {
                FindObjectOfType<AudioPlayer>().StopAllAudio();
                SceneManager.LoadScene("TitleScreen");
            }
            else
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
        }
    }
}
