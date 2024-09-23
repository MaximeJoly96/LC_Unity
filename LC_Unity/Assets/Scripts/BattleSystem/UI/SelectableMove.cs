using UnityEngine;
using TMPro;
using Abilities;
using Language;

namespace BattleSystem.UI
{
    public class SelectableMove : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        public void Feed(Ability ability)
        {
            _label.text = Localizer.Instance.GetString(ability.Name);
        }
    }
}
