using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;
using Language;

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

        public void ShowQuestDetails(Quest quest)
        {
            _questData = quest;

            _questNameLabel.text = Localizer.Instance.GetString(quest.NameKey);
            _questDescriptionLabel.text = Localizer.Instance.GetString(quest.DescriptionKey);

            _questStepsDisplay.Init(quest.Steps);
            _questRewardDisplay.Init(quest.Reward);
        }
    }
}
