﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Party;
using System.Linq;
using Language;
using Utils;

namespace Shop
{
    public class ItemDetails : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _itemType;
        [SerializeField]
        private TMP_Text _inStock;
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TMP_Text _itemDescription;

        public TMP_Text ItemName { get { return _itemName; } }
        public TMP_Text ItemDescription { get { return _itemDescription; } }
        public TMP_Text InStock { get { return _inStock; } }
        public TMP_Text ItemType { get { return _itemType; } }

        public void Feed(BaseItem item)
        {
            _itemName.text = Localizer.Instance.GetString(item.Name);
            // TODO : localiser la description + fix les tests
            _itemDescription.text = item.DetailedDescription();

            if (item.Category == ItemCategory.Weapon)
            {
                Weapon weapon = item as Weapon;
                _itemType.text = Localizer.Instance.GetString(weapon.Type.ToString().ToLower());
            }
            else if (item.Category == ItemCategory.Armour)
            {
                Armour armour = item as Armour;
                _itemType.text = Localizer.Instance.GetString(armour.Type.ToString().ToLower());
            }
            else
                _itemType.text = Localizer.Instance.GetString(item.Category.ToString().ToLower());

            InventoryItem inventoryItem = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == item.Id);
            if (inventoryItem != null)
                _inStock.text = Localizer.Instance.GetString("inStock") + " " + inventoryItem.InPossession.ToString();
            else
                _inStock.text = Localizer.Instance.GetString("inStock") + " " + 0;

            switch(item.Category)
            {
                case ItemCategory.Consumable:
                    _icon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.Icon);
                    break;
                case ItemCategory.Weapon:
                    _icon.sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.Icon);
                    break;
            }
        }

        public void Show(bool show)
        {
            _canvasGroup.alpha = show ? 1.0f : 0.0f;
        }

        public void SetItemNameObject(TMP_Text obj)
        {
            _itemName = obj;
        }
        public void SetItemDescriptionObject(TMP_Text obj)
        {
            _itemDescription = obj;
        }

        public void SetItemTypeObject(TMP_Text obj)
        {
            _itemType = obj;
        }

        public void SetInStockObject(TMP_Text obj)
        {
            _inStock = obj;
        }

        public void SetItemIconObject(Image obj)
        {
            _icon = obj;
        }

        public void SetCanvasGroupObject(CanvasGroup obj)
        {
            _canvasGroup = obj;
        }
    }
}
