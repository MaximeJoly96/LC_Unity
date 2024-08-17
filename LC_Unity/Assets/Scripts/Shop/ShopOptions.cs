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

        private Animator _animator { get { return GetComponent<Animator>(); } }

        public ShopOption Option { get { return _option; } }

        public void Hover(bool hover)
        {
            _animator.Play(hover ? "HoverOption" : "Idle");
        }

        public void Select()
        {
            _animator.Play("OptionSelected");
        }
    }
}
