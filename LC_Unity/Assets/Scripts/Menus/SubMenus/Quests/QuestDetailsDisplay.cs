using UnityEngine;
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

        public Quest QuestData { get { return _questData; } }
        public TMP_Text NameLabel { get { return _questNameLabel; } set { _questNameLabel = value; } }
        public TMP_Text DescriptionLabel { get { return _questDescriptionLabel; } set { _questDescriptionLabel = value; } }
        public QuestStepsDisplay StepsDisplay { get { return _questStepsDisplay; } set { _questStepsDisplay = value; } }
        public QuestRewardDisplay RewardsDisplay { get { return _questRewardDisplay; } set { _questRewardDisplay = value; } }

        public void ShowQuestDetails(Quest quest)
        {
            Clear();

            _questData = quest;

            _questNameLabel.text = Localizer.Instance.GetString(quest.NameKey);
            _questDescriptionLabel.text = Localizer.Instance.GetString(quest.DescriptionKey);

            _questStepsDisplay.Init(quest.Steps);
            _questRewardDisplay.Init(quest.Reward);
        }

        public void Clear()
        {
            _questData = null;

            _questNameLabel.text = string.Empty;
            _questDescriptionLabel.text = string.Empty;

            _questStepsDisplay.Clear();
            _questRewardDisplay.Clear();
        }
    }
}
