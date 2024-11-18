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
                _rewardComponents.Add(component);
            }

            if (reward.Gold > 0)
            {
                RewardComponentDisplay component = Instantiate(_rewardComponentPrefab, transform);
                component.Init(reward.Gold, SingleRewardType.Gold);
                _rewardComponents.Add(component);
            }

            for(int i = 0; i < reward.Items.Count; i++)
            {
                RewardComponentDisplay component = Instantiate(_rewardComponentPrefab, transform);
                component.Init(reward.Items[i]);
                _rewardComponents.Add(component);
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

        public void SetRewardComponentPrefab(RewardComponentDisplay component)
        {
            _rewardComponentPrefab = component;
        }

        public void UpdateVisualStatus(Color color)
        {
            for(int i = 0; i <  _rewardComponents.Count; i++)
            {
                _rewardComponents[i].UpdateVisualStatus(color);
            }
        }
    }
}
