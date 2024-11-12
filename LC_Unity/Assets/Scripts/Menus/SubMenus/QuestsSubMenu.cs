using Core;
using Utils;
using UnityEngine;
using Menus.SubMenus.Quests;

namespace Menus.SubMenus
{
    public class QuestsSubMenu : SubMenu
    {
        [SerializeField]
        protected QuestsHorizontalMenu _horizontalMenu;

        protected override void BindInputs()
        {
            _inputReceiver.OnCancel.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    CommonSounds.ActionCancelled();
                    Close();
                }
            });
        }

        protected override bool CanReceiveInput()
        {
            return !_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuQuestsTab;
        }

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
    }
}
