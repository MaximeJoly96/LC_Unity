using Actors;
using UnityEngine;
using TMPro;
using Language;

namespace Menus.SubMenus.Status
{
    public class EssenceAffinityPanel : StatusSubPanel
    {
        [SerializeField]
        private TMP_Text _affinityName;
        [SerializeField]
        private TMP_Text _description;

        public override void Feed(Character character)
        {
            _affinityName.text = Localizer.Instance.GetString(character.EssenceAffinity.Name);
            _description.text = Localizer.Instance.GetString(character.EssenceAffinity.Description);
        }
    }
}
