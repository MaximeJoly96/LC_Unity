using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Menus.SubMenus.Status
{
    public class AffinityDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _affinityName;
        [SerializeField]
        private TMP_Text _affinityMultiplier;

        public void Feed(Sprite icon, string name, float multiplier)
        {
            _icon.sprite = icon;
            _affinityName.text = name;
            _affinityMultiplier.text = (multiplier * 100.0f).ToString("0") + "%";
        }
    }
}
