using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Questing;
using System.Collections.Generic;

namespace Menus.SubMenus.Quests
{
    public class QuestRewardDisplay : MonoBehaviour
    {
        [SerializeField]
        private RewardComponentDisplay _rewardComponentPrefab;

        private List<RewardComponentDisplay> _rewardComponents;

        public List<RewardComponentDisplay> RewardComponents { get { return _rewardComponents; } }

        public void Init(QuestReward reward)
        {
            Clear();

            if(reward.Exp > 0)
            {
                RewardComponentDisplay component = Instantiate(_rewardComponentPrefab, transform);
                component.Init(reward.Exp, SingleRewardType.Experience);
            }

            if (reward.Gold > 0)
            {
                RewardComponentDisplay component = Instantiate(_rewardComponentPrefab, transform);
                component.Init(reward.Gold, SingleRewardType.Gold);
            }

            for(int i = 0; i < reward.Items.Count; i++)
            {
                RewardComponentDisplay component = Instantiate(_rewardComponentPrefab, transform);
                component.Init(reward.Items[i]);
            }
        }

        public void Clear()
        {
            _rewardComponents = new List<RewardComponentDisplay>();

            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
