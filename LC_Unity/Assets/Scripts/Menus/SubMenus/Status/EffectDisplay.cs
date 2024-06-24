using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Menus.SubMenus.Status
{
    public class EffectDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;

        public void Feed(Sprite icon, string name)
        {
            _icon.sprite = icon;
            _name.text = name;
        }
    }
}
