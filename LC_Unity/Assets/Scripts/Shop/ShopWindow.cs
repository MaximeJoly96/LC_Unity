using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;
using System.Collections.Generic;
using Core;
using Party;
using Inventory;
using System.Linq;
using Engine.MusicAndSounds;
using MusicAndSounds;

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
        private TMP_Text _currentGold;

        [SerializeField]
        private SelectableItem _selectableItemPreview;
        [SerializeField]
        private ShopConfirmationWindow _confirmationWindow;

        private int _optionsCursorPosition;
        private int _itemsListCursorPosition;
        private List<SelectableItem> _instItems;
        private Merchant _merchant;

        public void SetupMerchant(Merchant merchant)
        {
            _merchant = merchant;
            _shopName.text = merchant.Name;

            _partyPreview.gameObject.SetActive(_merchant.SoldItemsTypes.Contains(ItemCategory.Armour) ||
                                               _merchant.SoldItemsTypes.Contains(ItemCategory.Accessory) ||
                                               _merchant.SoldItemsTypes.Contains(ItemCategory.Weapon));

            UpdateGoldText();

            _optionsCursorPosition = 0;
            _itemsListCursorPosition = 0;

            foreach (var option in _options)
                option.Hover(false);

            _options[_optionsCursorPosition].Hover(true);
            _instItems = new List<SelectableItem>();

            _itemDetails.Show(false);

            _confirmationWindow.PurchaseCompleted.RemoveAllListeners();
            _confirmationWindow.PurchaseCompleted.AddListener(DoBuy);
            _confirmationWindow.PurchaseCancelled.RemoveAllListeners();
            _confirmationWindow.PurchaseCancelled.AddListener(CancelOrder);
            _confirmationWindow.SellCompleted.RemoveAllListeners();
            _confirmationWindow.SellCompleted.AddListener(DoSell);
        }

        public void MoveLeft()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                _optionsCursorPosition = _optionsCursorPosition == 0 ? _options.Length - 1 : --_optionsCursorPosition;
                UpdateCursors();
            }
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems || 
                     GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.MoveCursorLeft();
            }
        }

        public void MoveRight()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                _optionsCursorPosition = _optionsCursorPosition == _options.Length - 1 ? 0 : ++_optionsCursorPosition;
                UpdateCursors();
            }
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                     GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.MoveCursorRight();
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
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                    GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.MoveCursorUp();
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
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                     GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.MoveCursorDown();
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
                        ShowItemsToSell();
                        UpdateCursors();
                        break;
                    case ShopOption.Leave:
                        Cancel();
                        break;
                }
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList)
            {
                if (PartyManager.Instance.Gold < _instItems[_itemsListCursorPosition].Item.Price)
                {
                    PlayErrorSound();
                    return;
                }   

                Buy(_instItems[_itemsListCursorPosition].Item);
            }
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                if(_itemsListCursorPosition < _instItems.Count)
                    Sell(_instItems[_itemsListCursorPosition].Item);
            }
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems)
            {
                _confirmationWindow.ConfirmPurchase();
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.ConfirmSell();
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
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                     GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                _confirmationWindow.Cancel();
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
                if(_instItems.Count > 0)
                {
                    for (int i = 0; i < _instItems.Count; i++)
                    {
                        _instItems[i].Hover(i == _itemsListCursorPosition);
                    }

                    _itemDetails.Feed(_instItems[_itemsListCursorPosition].Item);
                    _itemDetails.Show(true);
                }
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
                if(_merchant.SoldItemsTypes.Contains(item.ItemData.Category))
                {
                    SelectableItem selectableItem = Instantiate(_selectableItemPreview, _scrollView.content);
                    selectableItem.Feed(item.ItemData);

                    selectableItem.Hover(false);

                    _instItems.Add(selectableItem);
                }
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

        private void Buy(BaseItem item)
        {
            _confirmationWindow.Open(item, true);
        }

        private void Sell(BaseItem item)
        {
            _confirmationWindow.Open(item, false);
        }

        private void DoBuy(BaseItem item, int quantity)
        {
            if(PartyManager.Instance.Gold < item.Price * quantity)
            {
                PlayErrorSound();
                return;
            }

            InventoryItem inventoryItem = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == item.Id);
            if (inventoryItem != null)
            {
                inventoryItem.ChangeAmount(quantity);
            }
            else
            {
                inventoryItem = new InventoryItem(item);
                inventoryItem.ChangeAmount(quantity);
                PartyManager.Instance.Inventory.Add(inventoryItem);
            }

            PartyManager.Instance.ChangeGold(new Engine.Party.ChangeGold { Value = -item.Price * quantity });
            _itemDetails.Feed(_instItems[_itemsListCursorPosition].Item);

            _confirmationWindow.Close();
            UpdateGoldText();
            PlayBuySellSound();
        }

        private void DoSell(BaseItem item, int quantity)
        {
            InventoryItem inventoryItem = PartyManager.Instance.Inventory.FirstOrDefault(i => i.ItemData.Id == item.Id);
            if (inventoryItem.InPossession < quantity)
            {
                PlayErrorSound();
                return;
            }

            inventoryItem.ChangeAmount(-quantity);
            if (inventoryItem.InPossession == 0)
                PartyManager.Instance.Inventory.Remove(inventoryItem);

            PartyManager.Instance.ChangeGold(new Engine.Party.ChangeGold { Value = item.Price * quantity });
            _itemDetails.Feed(_instItems[_itemsListCursorPosition].Item);

            _confirmationWindow.Close();
            UpdateGoldText(); 
            ShowItemsToSell();
            PlayBuySellSound();
        }

        private void CancelOrder()
        {
            _confirmationWindow.Close();
        }

        private void PlayErrorSound()
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = "Error1",
                Volume = 0.25f,
                Pitch = 1.0f
            });
        }

        private void PlayBuySellSound()
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = "MoneyTransfer",
                Volume = 0.25f,
                Pitch = 1.0f
            });
        }

        private void UpdateGoldText()
        {
            _currentGold.text = PartyManager.Instance.Gold + " " + (PartyManager.Instance.Gold > 1 ? Localizer.Instance.GetString("moneyLabelPlural") :
                                                                                                     Localizer.Instance.GetString("moneyLabel"));
        }
    }
}
