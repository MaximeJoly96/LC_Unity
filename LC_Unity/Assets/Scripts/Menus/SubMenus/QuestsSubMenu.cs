using Core;
using Utils;
using UnityEngine;
using Menus.SubMenus.Quests;
using UI;
using Questing;
using System.Collections;

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

            _horizontalMenu.QuestStatusWasSelected.AddListener((s) => 
            {
                FeedListOfQuests(s);
                _listOfQuests.HoverFirstItem();
            });

            _listOfQuests.Init();
            _listOfQuests.SelectionCancelled.RemoveAllListeners();
            _listOfQuests.SelectionCancelled.AddListener(() =>
            {
                StartCoroutine(SelectionCancelled());
            });
            _listOfQuests.SelectionChanged.RemoveAllListeners();
            _listOfQuests.SelectionChanged.AddListener(() => _detailsDisplay.ShowQuestDetails(_listOfQuests.SelectedQuest));

            _detailsDisplay.Clear();
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

                if(_listOfQuests.CreatedItems.Count > 0)
                    _detailsDisplay.ShowQuestDetails(_listOfQuests.SelectedQuest);
            }
        }

        protected IEnumerator SelectionCancelled()
        {
            _listOfQuests.Clear();
            _detailsDisplay.Clear();
            _horizontalMenu.UpdateCursorPosition();
            yield return new WaitForSeconds(0.1f);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuQuestsTab);
        }
    }
}
