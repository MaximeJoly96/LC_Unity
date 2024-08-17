using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;

namespace Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _shopName;
        [SerializeField]
        private ShopOptions[] _options;
        [SerializeField]
        private ScrollRect _scrollView;
        [SerializeField]
        private ItemDetails _itemDetails;
        [SerializeField]
        private PartyPreview _partyPreview;

        public void SetupMerchant(Merchant merchant)
        {
            _shopName.text = merchant.Name;
        }
    }
}
