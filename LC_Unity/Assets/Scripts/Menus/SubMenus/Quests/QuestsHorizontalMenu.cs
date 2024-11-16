using UI;
using Core;
using Questing;
using UnityEngine.Events;

namespace Menus.SubMenus.Quests
{
    public class QuestsHorizontalMenu : HorizontalMenu
    {
        private UnityEvent<QuestStatus> _questStatusWasSelected;

        public QuestStatus SelectedQuestStatus
        {
            get
            {
                return (_buttons[_cursorPosition] as QuestsHorizontalMenuButton).StatusType;
            }
        }

        public UnityEvent<QuestStatus> QuestStatusWasSelected
        {
            get
            {
                if (_questStatusWasSelected == null)
                    _questStatusWasSelected = new UnityEvent<QuestStatus>();

                return _questStatusWasSelected;
            }
        }

        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuQuestsTab;
        }

        public override void Init()
        {
            base.Init();

            foreach(var button in _buttons)
            {
                (button as QuestsHorizontalMenuButton).QuestStatusSelected.RemoveAllListeners();
                (button as QuestsHorizontalMenuButton).QuestStatusSelected.AddListener((s) => QuestStatusWasSelected.Invoke(s));
            }
        }
    }
}
