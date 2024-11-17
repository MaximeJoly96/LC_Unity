using Core;
using Utils;
using UnityEngine;
using Menus.SubMenus.Quests;
using UI;
using Questing;

namespace Menus.SubMenus
{
    public class QuestsSubMenu : SubMenu
    {
        [SerializeField]
        protected QuestsHorizontalMenu _horizontalMenu;
        [SerializeField]
        protected SelectableQuestsList _listOfQuests;
        [SerializeField]
        protected QuestDetailsDisplay _detailsDisplay;

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
            _horizontalMenu.QuestStatusWasSelected.AddListener((s) => FeedListOfQuests(s));
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);
        }

        protected void FeedListOfQuests(QuestStatus status)
        {
            if(!_busy)
            {
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.BrowsingQuests);

                switch(status)
                {
                    case QuestStatus.Running:
                        _listOfQuests.FeedQuests(QuestManager.Instance.RunningQuests);
                        break;
                    case QuestStatus.Completed:
                        _listOfQuests.FeedQuests(QuestManager.Instance.CompletedQuests);
                        break;
                    case QuestStatus.Failed:
                        _listOfQuests.FeedQuests(QuestManager.Instance.FailedQuests);
                        break;
                }

                _detailsDisplay.ShowQuestDetails(_listOfQuests.SelectedQuest);
            }
        }
    }
}
