using Actors;
using UnityEngine;
using TMPro;

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
            _affinityName.text = character.EssenceAffinity.Name;
            _description.text = character.EssenceAffinity.Description;
        }
    }
}
