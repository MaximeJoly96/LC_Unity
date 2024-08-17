using UnityEngine;
using TMPro;
using Inventory;

namespace Shop
{
    public class SelectableItem : MonoBehaviour
    {
        [SerializeField]
        private Transform _cursor;
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private TMP_Text _price;

        public BaseItem Item { get; private set; }

        private Animator _animator { get { return GetComponent<Animator>(); } }

        public void Feed(BaseItem item)
        {
            _itemName.text = item.Name;
            _price.text = item.Price.ToString();

            Item = item;
        }

        public void Hover(bool hover)
        {
            _animator.Play(hover ? "SelectableItemHover" : "Idle");
        }
    }
}
