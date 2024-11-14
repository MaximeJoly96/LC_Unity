using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;

namespace Menus.SubMenus.Quests
{
    public class IndividualQuestStepDisplay : MonoBehaviour
    {
        private QuestStep _questStepData;

        [SerializeField]
        private TMP_Text _questStepLabel;
        [SerializeField]
        private TMP_Text _questStepDescription;
        [SerializeField]
        private QuestStepRewardDisplay _questStepRewardDisplay;
    }
}
