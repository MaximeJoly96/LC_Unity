using UI;
using UnityEngine;

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

        public ShopOption Option { get { return _option; } set { _option = value; } }
    }
}
