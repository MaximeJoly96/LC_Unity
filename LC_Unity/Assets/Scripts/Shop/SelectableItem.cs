using UnityEngine;
using TMPro;
using Inventory;
using Language;
using UnityEngine.UI;
using Utils;

namespace Shop
{
    public class SelectableItem : MonoBehaviour
    {
        [SerializeField]
        private Transform _cursor;
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private TMP_Text _price;
        [SerializeField]
        private Image _icon;

        public BaseItem Item { get; private set; }

        private Animator _animator { get { return GetComponent<Animator>(); } }

        public void Feed(BaseItem item)
        {
            _itemName.text = Localizer.Instance.GetString(item.Name);
            _price.text = item.Price.ToString();

            Item = item;

            switch (item.Category)
            {
                case ItemCategory.Consumable:
                    _icon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.Icon);
                    break;
                case ItemCategory.Weapon:
                    _icon.sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.Icon);
                    break;
            }
        }

        public void Hover(bool hover)
        {
            _animator.Play(hover ? "SelectableItemHover" : "Idle");
        }
    }
}
