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

        public ShopOption Option { get { return _option; } set { _option = value; } }

        public void Hover(bool hover)
        {
            if(_animator)
                _animator.Play(hover ? "HoverOption" : "Idle");
        }

        public void Select()
        {
            if(_animator)
                _animator.Play("OptionSelected");
        }
    }
}
