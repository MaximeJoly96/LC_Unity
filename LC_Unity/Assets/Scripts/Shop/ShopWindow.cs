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
using Utils;

namespace Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _shopName;
        [SerializeField]
        private ShopHorizontalMenu _shopHorizontalMenu;
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

        private int _itemsListCursorPosition;
        private List<SelectableItem> _instItems;
        private Merchant _merchant;

        public string ShopName { get { return _shopName.text;} }
        public string CurrentGold { get { return _currentGold.text; } }
        public ItemDetails ItemDetails { get { return _itemDetails; } }

        public void SetupMerchant(Merchant merchant)
        {
            _merchant = merchant;
            _shopName.text = Localizer.Instance.GetString(merchant.Name);

            _partyPreview.gameObject.SetActive(_merchant.SoldItemsTypes.Contains(ItemCategory.Armour) ||
                                               _merchant.SoldItemsTypes.Contains(ItemCategory.Accessory) ||
                                               _merchant.SoldItemsTypes.Contains(ItemCategory.Weapon));

            UpdateGoldText();

            _itemsListCursorPosition = 0;

            _shopHorizontalMenu.Init();
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
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems || 
                GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                CommonSounds.CursorMoved();
                _confirmationWindow.MoveCursorLeft();
            }
        }

        public void MoveRight()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                CommonSounds.CursorMoved();
                _confirmationWindow.MoveCursorRight();
            }
        }

        public void MoveUp()
        {
            if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
               GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                CommonSounds.CursorMoved();
                _itemsListCursorPosition = _itemsListCursorPosition == 0 ? _instItems.Count - 1 : --_itemsListCursorPosition;
                UpdateCursors();
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                    GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                CommonSounds.CursorMoved();
                _confirmationWindow.MoveCursorUp();
            }
        }

        public void MoveDown()
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                CommonSounds.CursorMoved();
                _itemsListCursorPosition = _itemsListCursorPosition == _instItems.Count - 1 ? 0 : ++_itemsListCursorPosition;
                UpdateCursors();
            }
            else if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                     GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems)
            {
                CommonSounds.CursorMoved();
                _confirmationWindow.MoveCursorDown();
            }
        }

        public void Select()
        {
            /*if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                switch (_options[_optionsCursorPosition].Option)
                {
                    case ShopOption.Buy:
                        CommonSounds.OptionSelected();
                        GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShopBuyList);
                        _options[_optionsCursorPosition].SelectButton();
                        ShowItemsToBuy();
                        UpdateCursors();
                        break;
                    case ShopOption.Sell:
                        if(!PartyManager.Instance.Inventory.Any(i => _merchant.SoldItemsTypes.Contains(i.ItemData.Category)))
                        {
                            PlayErrorSound();
                            return;
                        }

                        CommonSounds.OptionSelected();
                        GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InShopSellList);
                        _options[_optionsCursorPosition].SelectButton();
                        ShowItemsToSell();
                        UpdateCursors();
                        break;
                    case ShopOption.Leave:
                        Cancel();
                        break;
                }
            }
            else */if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList)
            {
                if (PartyManager.Instance.Gold < _instItems[_itemsListCursorPosition].Item.Price)
                {
                    PlayErrorSound();
                    return;
                }

                CommonSounds.OptionSelected();
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
            CommonSounds.ActionCancelled();
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions)
            {
                FindObjectOfType<ShopLoader>().CloseShop();
            }
            else if(GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                    GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList)
            {
                ClearItemsList();
                _itemDetails.Show(false);
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
                Volume = 0.75f,
                Pitch = 1.0f
            });
        }

        private void PlayBuySellSound()
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = "MoneyTransfer",
                Volume = 0.75f,
                Pitch = 1.0f
            });
        }

        private void UpdateGoldText()
        {
            _currentGold.text = PartyManager.Instance.Gold + " " + (PartyManager.Instance.Gold > 1 ? Localizer.Instance.GetString("moneyLabelPlural") :
                                                                                                     Localizer.Instance.GetString("moneyLabel"));
        }

        public void SetOptions(IEnumerable<ShopHorizontalMenuButton> options)
        {
            //_options = options.ToArray();
        }

        public void SetShopNameObject(TMP_Text obj)
        {
            _shopName = obj;
        }

        public void SetScrollViewObject(ScrollRect obj)
        {
            _scrollView = obj;
        }

        public void SetCurrentGoldObject(TMP_Text obj)
        {
            _currentGold = obj;
        }

        public void SetPartyPreviewObject(PartyPreview obj)
        {
            _partyPreview = obj;
        }

        public void SetItemDetailsObject(ItemDetails obj)
        {
            _itemDetails = obj;
        }

        public void SetConfirmationWindowObject(ShopConfirmationWindow obj)
        {
            _confirmationWindow = obj;
        }
    }
}
