using UnityEngine;
using TMPro;
using Abilities;
using Language;

namespace BattleSystem.UI
{
    public class SelectableMoveCategory : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        public AbilityCategory Category { get; private set; }
        public TMP_Text Label { get { return _label; } set { _label = value; } }

        public void Feed(AbilityCategory category)
        {
            Category = category;
            _label.text = Localizer.Instance.GetString(category.ToString());
        }
    }
}
