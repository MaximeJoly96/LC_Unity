using UnityEngine;
using Inputs;
using Core;
using Engine.SceneControl;
using System.Linq;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        private const float SELECTION_DELAY = 0.2f; // seconds

        private float _selectionDelay;
        private bool _delayOn;
        private TextAsset _currentMerchants;

        [SerializeField]
        private ShopWindow _shopWindow;

        private void Start()
        {
            FindObjectOfType<InputController>().ButtonClicked.AddListener(HandleInputs);

            _selectionDelay = 0.0f;
            _delayOn = false;
        }

        private void HandleInputs(InputAction input)
        {
            if(!_delayOn && InShopState())
            {
                switch(input)
                {
                    case InputAction.Select:
                        _shopWindow.Select();
                        break;
                    case InputAction.Cancel:
                        _shopWindow.Cancel();
                        break;
                    case InputAction.MoveLeft:
                        _shopWindow.MoveLeft();
                        break;
                    case InputAction.MoveRight:
                        _shopWindow.MoveRight();
                        break;
                    case InputAction.MoveUp:
                        _shopWindow.MoveUp();
                        break;
                    case InputAction.MoveDown:
                        _shopWindow.MoveDown();
                        break;
                }

                _delayOn = true;
            }
        }

        protected void Update()
        {
            if (_delayOn)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }

        public void SetupShop(ShopProcessing shop)
        {
            MerchantParser parser = new MerchantParser();
            Merchant merchant = parser.ParseMerchants(_currentMerchants).FirstOrDefault(m => m.Id == shop.MerchantId);

            _shopWindow.SetupMerchant(merchant);
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
