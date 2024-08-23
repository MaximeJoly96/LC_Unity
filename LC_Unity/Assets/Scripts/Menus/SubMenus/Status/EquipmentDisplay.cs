using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Language;

namespace Menus.SubMenus.Status
{
    public class EquipmentDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;

        public void Feed(Sprite icon, string name)
        {
            _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, icon != null ? 1.0f : 0.0f);
            _icon.sprite = icon;
            _name.text = name == "" ? Localizer.Instance.GetString("noEquipment") : name;
        }
    }
}
