using UI;
using Questing;
using UnityEngine;
using UnityEngine.Events;

namespace Menus.SubMenus.Quests
{
    public class QuestsHorizontalMenuButton : HorizontalMenuButton
    {
        private UnityEvent<QuestStatus> _questStatusSelected;

        [SerializeField]
        private QuestStatus _statusType;

        public QuestStatus StatusType { get { return _statusType; } set { _statusType = value; } }
        public UnityEvent<QuestStatus> QuestStatusSelected
        {
            get
            {
                if (_questStatusSelected == null)
                    _questStatusSelected = new UnityEvent<QuestStatus>();

                return _questStatusSelected;
            }
        }

        public override void SelectButton()
        {
            base.SelectButton();
            QuestStatusSelected.Invoke(StatusType);
        }
    }
}
