using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;
using System.Collections.Generic;
using Core;
using Party;

namespace Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _shopName;
        [SerializeField]
        private ShopOptions[] _options;
        [SerializeField]
        private ScrollRect _scrollView;
        [SerializeField]
        private ItemDetails _itemDetails;
        [SerializeField]
        private PartyPreview _partyPreview;

        [SerializeField]
        private SelectableItem _selectableItemPreview;

        private int _optionsCursorPosition;
        private int _itemsListCursorPosition;
        private List<SelectableItem> _instItems;
        private Merchant _merchant;

        public void SetupMerchant(Merchant merchant)
        {
            _merchant = merchant;
            _shopName.text = merchant.Name;

            _optionsCursorPosition = 0;
            _itemsListCursorPosition = 0;

            foreach (var option in _options)
                option.Hover(false);

            _options[_optionsCursorPosition].Hover(true);
            _instItems = new List<SelectableItem>();

            _itemDetails.Show(false);
        }

        public void MoveLeft()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                _optionsCursorPosition = _optionsCursorPosition == 0 ? _options.Length - 1 : --_optionsCursorPosition;
                UpdateCursors();
            }
        }

        public void MoveRight()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                _optionsCursorPosition = _optionsCursorPosition == _options.Length - 1 ? 0 : ++_optionsCursorPosition;
                UpdateCursors();
            } 
        }

        public void MoveUp()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
               GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                _itemsListCursorPosition = _itemsListCursorPosition == 0 ? _instItems.Count - 1 : --_itemsListCursorPosition;
                UpdateCursors();
            }
        }

        public void MoveDown()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                _itemsListCursorPosition = _itemsListCursorPosition == _instItems.Count - 1 ? 0 : ++_itemsListCursorPosition;
                UpdateCursors();
            }  
        }

        public void Select()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                switch (_options[_optionsCursorPosition].Option)
                {
                    case ShopOption.Buy:
                        GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShopBuyList);
                        _options[_optionsCursorPosition].Select();
                        ShowItemsToBuy();
                        UpdateCursors();
                        break;
                    case ShopOption.Sell:
                        GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShopSellList);
                        _options[_optionsCursorPosition].Select();
                        break;
                    case ShopOption.Leave:
                        Cancel();
                        break;
                }
            }
        }

        public void Cancel()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                FindObjectOfType<ShopLoader>().CloseShop();
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                    GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                _options[_optionsCursorPosition].Hover(true);
                ClearItemsList();
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShopOptions);
            }
        }

        private void UpdateCursors()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                for (int i = 0; i < _options.Length; i++)
                {
                    _options[i].Hover(i == _optionsCursorPosition);
                }

                if (_instItems.Count > 0)
                {
                    _instItems[0].Hover(true);
                    _itemDetails.Feed(_instItems[0].Item);
                }

                _itemDetails.Show(false);
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                    GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                for(int i = 0; i < _instItems.Count; i++)
                {
                    _instItems[i].Hover(i == _itemsListCursorPosition);
                }

                _itemDetails.Feed(_instItems[_itemsListCursorPosition].Item);
                _itemDetails.Show(true);
            }
        }

        private void ShowItemsToBuy()
        {
            _itemsListCursorPosition = 0;
            ClearItemsList();

            foreach (var item in _merchant.Items)
            {
                SelectableItem selectableItem = Instantiate(_selectableItemPreview, _scrollView.content);
                selectableItem.Feed(item);

                selectableItem.Hover(false);

                _instItems.Add(selectableItem);
            }

            if(_instItems.Count > 0)
                _instItems[0].Hover(true);
        }

        private void ShowItemsToSell()
        {
            _itemsListCursorPosition = 0;
            ClearItemsList();

            foreach(var item in PartyManager.Instance.Inventory)
            {
                SelectableItem selectableItem = Instantiate(_selectableItemPreview, _scrollView.content);
                selectableItem.Feed(item.ItemData);

                selectableItem.Hover(false);

                _instItems.Add(selectableItem);
            }
        }

        private void ClearItemsList()
        {
            _instItems.Clear();

            foreach(Transform child in _scrollView.content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
