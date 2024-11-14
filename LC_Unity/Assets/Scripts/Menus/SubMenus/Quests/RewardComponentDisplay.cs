﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Language;

namespace Menus.SubMenus.Quests
{
    public enum SingleRewardType
    {
        Experience,
        Gold
    }

    public class RewardComponentDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _quantity;

        public void SetQuantity(int quantity)
        {
            _quantity.text = quantity.ToString();
        }

        public void Init(int value, SingleRewardType rewardType)
        {
            switch(rewardType)
            {
                case SingleRewardType.Experience:
                    _label.text = Localizer.Instance.GetString("experience");
                    break;
                case SingleRewardType.Gold:
                    _label.text = Localizer.Instance.GetString(value > 1 ? "moneyLabelPlural" : "moneyLabel");
                    break;
            }
        }

        public void Init(InventoryItem item)
        {
            _label.text = Localizer.Instance.GetString(item.ItemData.Name);
        }

        public void SetIcon(int iconId)
        {

        }
    }
}
