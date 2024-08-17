using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Shop
{
    public class ItemDetails : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _itemName;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _itemType;
        [SerializeField]
        private TMP_Text _inStock;

        [SerializeField]
        private TMP_Text _itemDescription;
    }
}
