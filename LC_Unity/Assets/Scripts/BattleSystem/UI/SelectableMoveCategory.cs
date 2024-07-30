using UnityEngine;
using TMPro;
using Abilities;

namespace BattleSystem.UI
{
    public class SelectableMoveCategory : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        public AbilityCategory Category { get; private set; }

        public void Feed(AbilityCategory category)
        {
            Category = category;
            _label.text = category.ToString();
        }
    }
}
