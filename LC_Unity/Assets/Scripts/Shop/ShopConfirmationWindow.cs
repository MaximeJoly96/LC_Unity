﻿using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Core;
using Inventory;
using Language;
using UnityEngine.Events;

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
        private QuantitySelector _firstDigit;
        [SerializeField]
        private QuantitySelector _secondDigit;

        private bool _buying;
        private int _currentPositionCursor;
        private BaseItem _currentItem;

        private UnityEvent<BaseItem, int> _purchaseCompleted;
        private UnityEvent _purchaseCancelled;

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
            GlobalStateMachine.Instance.UpdateState(_buying ? GlobalStateMachine.State.BuyingItems : GlobalStateMachine.State.SellingItems);
            _animator.Play("OpenConfirmationWindow");
        }

        public void Close()
        {
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

        public void Confirm()
        {
            PurchaseCompleted.Invoke(_currentItem, _firstDigit.Value * 10 + _secondDigit.Value);
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
    }
}
