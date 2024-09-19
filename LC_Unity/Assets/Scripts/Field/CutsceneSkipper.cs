using UnityEngine;
using Inputs;
using MsgBox;
using Core;
using Language;
using Engine.SystemSettings;
using Engine.Events;

namespace Field
{
    public class CutsceneSkipper : MonoBehaviour
    {
        private AllowCutsceneSkip _currentSkip;

        public bool CanSkipScene { get { return _currentSkip.Allow; } }

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(AttemptToSkip);
        }

        private void AttemptToSkip(InputAction action)
        {
            if(CanSkipScene)
            {
                switch (action)
                {
                    case InputAction.OpenMenu:
                        PromptToSkip();
                        break;
                }
            }
        }

        public void EnableSceneSkipping(AllowCutsceneSkip allow)
        {
            _currentSkip = allow;
        }

        private void PromptToSkip()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.PromptedToSkipScene);

            MessageBoxService.Instance.MessageBoxClosedWithResult.RemoveAllListeners();
            MessageBoxService.Instance.ShowYesNoMessage(Localizer.Instance.GetString("promptCutsceneSkip"), MessageBoxType.Warning);
            MessageBoxService.Instance.MessageBoxClosedWithResult.AddListener(ProceedToSkip);
        }

        private void ProceedToSkip(MessageBoxAnswer answer)
        {
            switch(answer)
            {
                case MessageBoxAnswer.Yes:
                    GetComponent<EventsRunner>().RunEvents(_currentSkip.ActionsWhenSkipping);
                    break;
                case MessageBoxAnswer.No:
                    GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
                    break;
            }
        }
    }
}
