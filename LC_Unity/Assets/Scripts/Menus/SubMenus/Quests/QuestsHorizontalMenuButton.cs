using UI;
using Questing;
using UnityEngine;

namespace Menus.SubMenus.Quests
{
    public class QuestsHorizontalMenuButton : HorizontalMenuButton
    {
        [SerializeField]
        private QuestStatus _statusType;

        public QuestStatus StatusType { get { return _statusType; } }
    }
}
