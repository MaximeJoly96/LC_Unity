using Inventory;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Language;
using Utils;

namespace BattleSystem.UI
{
    public class BattleInventoryItem : BattleMenuItem
    {
        [SerializeField]
        private TMP_Text _quantity;
        [SerializeField]
        private Image _icon;

        private InventoryItem _item;

        public void Feed(InventoryItem item)
        {
            _item = item;

            _label.text = Localizer.Instance.GetString(item.ItemData.Name);
            _quantity.text = "x" + item.InPossession;
            _icon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.ItemData.Icon);
        }
    }
}
