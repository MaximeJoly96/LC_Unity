using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Core;
using Inventory;
using Language;
using UnityEngine.Events;
using Party;
using System.Linq;
using Utils;

namespace Shop
{
    public class ShopConfirmationWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private Image _itemIcon;
        [SerializeField]
        private TMP_Text _itemPrice;
        [SerializeField]
        private TMP_Text _totalPrice;
        [SerializeField]
        private LocalizedText _selectItemQuantity;
        [SerializeField]
        private TMP_Text _inStock;

        [SerializeField]
        private QuantitySelector _firstDigit;
        [SerializeField]
        private QuantitySelector _secondDigit;

        private bool _buying;
        private int _currentPositionCursor;
        private BaseItem _currentItem;

        private UnityEvent<BaseItem, int> _purchaseCompleted;
        private UnityEvent _purchaseCancelled;
        private UnityEvent<BaseItem, int> _sellCompleted;

        public UnityEvent<BaseItem, int> PurchaseCompleted
        {
            get
            {
                if (_purchaseCompleted == null)
                    _purchaseCompleted = new UnityEvent<BaseItem, int>();

                return _purchaseCompleted;
            }
        }

        public UnityEvent PurchaseCancelled
        {
            get
            {
                if(_purchaseCancelled == null)
                    _purchaseCancelled = new UnityEvent();

                return _purchaseCancelled;
            }
        }

        public UnityEvent<BaseItem, int> SellCompleted
        {
            get
            {
                if (_sellCompleted == null)
                    _sellCompleted = new UnityEvent<BaseItem, int>();

                return _sellCompleted;
            }
        }

        public string SelectedItemQuantity { get { return _selectItemQuantity.GetComponent<TextMeshProUGUI>().text; } }
        public string ItemName { get { return _itemName.text; } }
        public string ItemPrice { get { return _itemPrice.text; } }
        public string TotalPrice { get { return _totalPrice.text; } }

        private Animator _animator
        {
            get { return GetComponent<Animator>(); }
        }

        public void Open(BaseItem item, bool buying)
        {
            Feed(item);

            _currentPositionCursor = 1;
            UpdateDigits();

            _firstDigit.SetValue(0);
            _secondDigit.SetValue(1);
            UpdateTotalPrice();

            _buying = buying;
            _selectItemQuantity.UpdateKey(_buying ? "wishToBuy" : "wishToSell");
            GlobalStateMachine.Instance.UpdateState(_buying ? GlobalStateMachine.State.BuyingItems : GlobalStateMachine.State.SellingItems);

            if(_animator)
                _animator.Play("OpenConfirmationWindow");
        }

        public void Close()
        {
            if(_animator)
                _animator.Play("CloseConfirmationWindow");
        }

        public void FinishedOpening()
        {

        }

        public void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(_buying ? GlobalStateMachine.State.InShopBuyList : GlobalStateMachine.State.InShopSellList);
        }

        public void MoveCursorLeft()
        {
            _currentPositionCursor = _currentPositionCursor == 0 ? 1 : 0;
            UpdateDigits();
        }

        public void MoveCursorRight()
        {
            _currentPositionCursor = _currentPositionCursor == 1 ? 0 : 1;
            UpdateDigits();
        }

        public void MoveCursorUp()
        {
            if (_currentPositionCursor == 0)
                _firstDigit.Increment();
            else
                _secondDigit.Increment();

            UpdateTotalPrice();
        }

        public void MoveCursorDown()
        {
            if (_currentPositionCursor == 0)
                _firstDigit.Decrement();
            else
                _secondDigit.Decrement();

            UpdateTotalPrice();
        }

        public void ConfirmPurchase()
        {
            PurchaseCompleted.Invoke(_currentItem, _firstDigit.Value * 10 + _secondDigit.Value);
        }

        public void ConfirmSell()
        {
            SellCompleted.Invoke(_currentItem, _firstDigit.Value * 10 + _secondDigit.Value);
        }

        public void Cancel()
        {
            PurchaseCancelled.Invoke();
        }

        private void Feed(BaseItem item)
        {
            _currentItem = item;

            _itemName.text = Localizer.Instance.GetString(item.Name);
            _itemPrice.text = item.Price + " " + (item.Price > 1 ? Localizer.Instance.GetString("moneyLabelPlural") :
                                                                   Localizer.Instance.GetString("moneyLabel"));

            switch (item.Category)
            {
                case ItemCategory.Consumable:
                    _itemIcon.sprite = FindObjectOfType<ConsumablesWrapper>().GetSpriteForConsumable(item.Icon);
                    break;
                case ItemCategory.Weapon:
                    _itemIcon.sprite = FindObjectOfType<WeaponsWrapper>().GetSpriteForWeapon(item.Icon);
                    break;
            }

            InventoryItem inventoryItem = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == item.Id);

            if (inventoryItem != null)
                _inStock.text = Localizer.Instance.GetString("inStock") + " " + inventoryItem.InPossession;
            else
                _inStock.text = Localizer.Instance.GetString("inStock") + " 0";

            UpdateTotalPrice();
        }

        private void UpdateDigits()
        {
            _firstDigit.Select(_currentPositionCursor == 0);
            _secondDigit.Select(_currentPositionCursor == 1);
        }

        private void UpdateTotalPrice()
        {
            int quantity = _firstDigit.Value * 10 + _secondDigit.Value;
            int total = quantity * _currentItem.Price;

            _totalPrice.text = total + " " + (total > 1 ? Localizer.Instance.GetString("moneyLabelPlural") :
                                                          Localizer.Instance.GetString("moneyLabel"));
        }

        public void SetItemNameObject(TMP_Text obj)
        {
            _itemName = obj;
        }

        public void SetItemIconObject(Image obj)
        {
            _itemIcon = obj;
        }

        public void SetItemPriceObject(TMP_Text obj)
        {
            _itemPrice = obj;
        }

        public void SetTotalPriceObject(TMP_Text obj)
        {
            _totalPrice = obj;
        }

        public void SetInStockObject(TMP_Text obj)
        {
            _inStock = obj;
        }

        public void SetSelectedQuantityObject(LocalizedText obj)
        {
            _selectItemQuantity = obj;
        }

        public void SetFirstDigitObject(QuantitySelector selectorObj)
        {
            _firstDigit = selectorObj;
        }

        public void SetSecondDigitObject(QuantitySelector selectorObj)
        {
            _secondDigit = selectorObj;
        }
    }
}
