using UnityEngine;
using Core;
using Engine.SceneControl;
using System.Linq;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        private TextAsset _currentMerchants;
        private InputReceiver _inputReceiver;

        [SerializeField]
        private ShopWindow _shopWindow;

        private void Start()
        {
            BindInputs();
        }

        private void BindInputs()
        {
            _inputReceiver = GetComponent<InputReceiver>();

            _inputReceiver.OnSelect.AddListener(() =>
            {
                if(CanReceiveInput())
                {
                    _shopWindow.Select();
                }
            });

            _inputReceiver.OnCancel.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _shopWindow.Cancel();
                }
            });

            _inputReceiver.OnMoveDown.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _shopWindow.MoveDown();
                }
            });

            _inputReceiver.OnMoveUp.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _shopWindow.MoveUp();
                }
            });

            _inputReceiver.OnMoveLeft.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _shopWindow.MoveLeft();
                }
            });

            _inputReceiver.OnMoveRight.AddListener(() =>
            {
                if (CanReceiveInput())
                {
                    _shopWindow.MoveRight();
                }
            });

        }

        private bool CanReceiveInput()
        {
            return InShopState();
        }

        public void SetupShop(ShopProcessing shop)
        {
            MerchantParser parser = new MerchantParser();
            Merchant merchant = parser.ParseMerchants(_currentMerchants).FirstOrDefault(m => m.Id == shop.MerchantId);

            _shopWindow.SetupMerchant(merchant);
        }

        public void SetupWindow(ShopWindow shopWindow)
        {
            _shopWindow = shopWindow;
        }

        public void LoadMerchants(TextAsset merchants)
        {
            _currentMerchants = merchants;
        }

        private bool InShopState()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions ||
                   GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopBuyList ||
                   GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopSellList ||
                   GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BuyingItems ||
                   GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.SellingItems;
        }
    }
}
