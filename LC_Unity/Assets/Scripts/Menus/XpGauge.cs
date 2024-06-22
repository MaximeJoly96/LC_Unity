using UnityEngine;
using TMPro;

namespace Menus
{
    public class XpGauge : StatGauge
    {
        [SerializeField]
        protected TMP_Text _level;

        public void SetLevel(float xp, float requiredXp, int currentLevel)
        {
            float ratio = xp / requiredXp;

            _filledGauge.fillAmount = ratio;
            _value.text = (ratio * 100.0f).ToString("0") + "%";
            _level.text = "Lv. " + (currentLevel + 1);
        }
    }
}
