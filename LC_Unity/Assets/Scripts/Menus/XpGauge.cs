using UnityEngine;
using TMPro;
using Language;
using UnityEngine.UI;

namespace Menus
{
    public class XpGauge : StatGauge
    {
        [SerializeField]
        protected TMP_Text _level;

        public TMP_Text LevelLabel { get { return _level; } }

        public void SetLevel(float xp, float requiredXp, int currentLevel)
        {
            if(xp > requiredXp)
                xp = requiredXp;

            float ratio = Mathf.Abs(0.0f - requiredXp) < 0.01f ? 0.0f : xp / requiredXp;

            _filledGauge.fillAmount = ratio;
            _value.text = (ratio * 100.0f).ToString("0") + "%";
            _level.text = Localizer.Instance.GetString("levelShort") + " " + (currentLevel + 1);
        }

        // For unit testing purposes
        public void Init(Image gauge, TMP_Text text, TMP_Text levelText)
        {
            Init(gauge, text);
            _level = levelText;
        }
    }
}
