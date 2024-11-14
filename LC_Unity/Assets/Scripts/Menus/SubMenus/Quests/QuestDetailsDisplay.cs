using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;

namespace Menus.SubMenus.Quests
{
    public class QuestDetailsDisplay : MonoBehaviour
    {
        private Quest _questData;

        [SerializeField]
        private TMP_Text _questNameLabel;
        [SerializeField]
        private TMP_Text _questDescriptionLabel;

        [SerializeField]
        private QuestStepsDisplay _questStepsDisplay;
        [SerializeField]
        private QuestRewardDisplay _questRewardDisplay;

    }
}
