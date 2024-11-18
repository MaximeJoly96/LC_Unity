using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;
using Language;

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
        private QuestRewardDisplay _questStepRewardDisplay;

        public QuestStep QuestStepData { get { return _questStepData; } }
        public TMP_Text StepLabel { get { return _questStepLabel; } set { _questStepLabel = value; } }
        public TMP_Text StepDescription { get { return _questStepDescription; } set { _questStepDescription = value; } }
        public QuestRewardDisplay RewardDisplay { get { return _questStepRewardDisplay; } set { _questStepRewardDisplay = value; } }

        public void Init(QuestStep stepData)
        {
            _questStepData = stepData;

            _questStepLabel.text = Localizer.Instance.GetString(stepData.NameKey);
            _questStepDescription.text = Localizer.Instance.GetString(stepData.DescriptionKey);

            _questStepRewardDisplay.Init(stepData.Reward);

            UpdateVisualStatus(stepData);
        }

        public void UpdateVisualStatus(QuestStep stepData)
        {
            Color color = Color.white;

            switch(stepData.Status)
            {
                case QuestStepStatus.Failed:
                    color = Color.red;
                    break;
                case QuestStepStatus.Locked:
                    color = Color.clear;
                    break;
                case QuestStepStatus.Completed:
                    color = Color.green;
                    break;
            }

            _questStepLabel.color = color;
            _questStepDescription.color = color;

            _questStepRewardDisplay.UpdateVisualStatus(color);
        }
    }
}
