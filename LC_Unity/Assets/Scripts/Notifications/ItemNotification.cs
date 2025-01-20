using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Language;
using Utils;

namespace Notifications
{
    public class ItemNotification : Notification
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Image _icon;

        public void Feed(BaseItem item, int change)
        {
            if(change != 0)
            {
                _text.text = Localizer.Instance.GetString(item.Name) + " " + (change > 0 ? "+" : "") + change;

                Sprite sprite = null;

                switch(item.Category)
                {
                    case ItemCategory.Accessory:
                        break;
                    case ItemCategory.Resource:
                        break;
                    case ItemCategory.Weapon:
                        sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.Icon);
                        break;
                    case ItemCategory.KeyItem:
                        break;
                    case ItemCategory.Consumable:
                        sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.Icon); 
                        break;
                    case ItemCategory.Armour:
                        break;
                }

                _icon.sprite = sprite;
            }
        }

        
    }
}
