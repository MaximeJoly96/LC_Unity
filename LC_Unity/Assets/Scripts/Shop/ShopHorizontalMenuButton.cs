using Core;
using Party;
using UI;
using UnityEngine;
using Utils;
using UnityEngine.Events;

namespace Shop
{
    public enum ShopOption
    {
        Buy,
        Sell,
        Leave
    }

    public class ShopHorizontalMenuButton : HorizontalMenuButton
    {
        [SerializeField]
        private ShopOption _option;

        private UnityEvent<ShopOption> _optionSelected;

        public ShopOption Option { get { return _option; } set { _option = value; } }
        public UnityEvent<ShopOption> OptionSelected
        {
            get
            {
                if(_optionSelected == null)
                    _optionSelected = new UnityEvent<ShopOption>();

                return _optionSelected;
            }
        }

        public override void SelectButton()
        {
            OptionSelected.Invoke(Option);
        }
    }
}
