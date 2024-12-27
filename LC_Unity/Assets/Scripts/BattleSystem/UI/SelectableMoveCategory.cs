using UnityEngine;
using TMPro;
using Abilities;
using Language;
using UnityEngine.UI;

namespace BattleSystem.UI
{
    public class SelectableMoveCategory : BattleMenuItem
    {
        public AbilityCategory Category { get; private set; }
        public TMP_Text Label { get { return _label; } set { _label = value; } }

        public void Feed(AbilityCategory category)
        {
            Category = category;
            _label.text = Localizer.Instance.GetString(category.ToString());

            if (Category == AbilityCategory.FleeCommand && !BattleDataHolder.Instance.BattleData.CanEscape)
                _label.color = Color.grey;
        }
    }
}
