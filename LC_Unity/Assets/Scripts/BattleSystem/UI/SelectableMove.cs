using UnityEngine;
using TMPro;
using Abilities;

namespace BattleSystem.UI
{
    public class SelectableMove : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        public void Feed(Ability ability)
        {
            _label.text = ability.Name;
        }
    }
}
