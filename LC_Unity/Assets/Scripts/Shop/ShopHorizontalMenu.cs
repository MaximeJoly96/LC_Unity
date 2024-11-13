using Core;
using UI;
using UnityEngine.Events;

namespace Shop
{
    public class ShopHorizontalMenu : HorizontalMenu
    {
        private UnityEvent<ShopOption> _optionSelected;

        public UnityEvent<ShopOption> OptionSelected
        {
            get
            {
                if (_optionSelected == null)
                    _optionSelected = new UnityEvent<ShopOption>();

                return _optionSelected;
            }
        }

        public override void Init()
        {
            base.Init();

            foreach (HorizontalMenuButton but in Buttons)
            {
                ShopHorizontalMenuButton shopButton = but as ShopHorizontalMenuButton;
                shopButton.OptionSelected.RemoveAllListeners();
                shopButton.OptionSelected.AddListener(o => OptionSelected.Invoke(o));
            }
        }

        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions;
        }
    }
}
