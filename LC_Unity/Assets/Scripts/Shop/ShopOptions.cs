using UnityEngine;

namespace Shop
{
    public enum ShopOption
    {
        Buy,
        Sell,
        Leave
    }

    public class ShopOptions : MonoBehaviour
    {
        [SerializeField]
        private ShopOption _option;

        public ShopOption Option { get { return _option; } }
    }
}
