using UnityEngine;
using TMPro;
using Actors;

namespace Menus.SubMenus.Status
{
    public class StatsPanel : StatusSubPanel
    {
        [SerializeField]
        private TMP_Text _health;
        [SerializeField]
        private TMP_Text _mana;
        [SerializeField]
        private TMP_Text _essence;
        [SerializeField]
        private TMP_Text _strength;
        [SerializeField]
        private TMP_Text _defense;
        [SerializeField]
        private TMP_Text _magic;
        [SerializeField]
        private TMP_Text _magicDefense;
        [SerializeField]
        private TMP_Text _agility;
        [SerializeField]
        private TMP_Text _luck;
        [SerializeField]
        private TMP_Text _critChance;
        [SerializeField]
        private TMP_Text _evasion;
        [SerializeField]
        private TMP_Text _parry;
        [SerializeField]
        private TMP_Text _provocation;
        [SerializeField]
        private TMP_Text _critDamage;
        [SerializeField]
        private TMP_Text _accuracy;
        [SerializeField]
        private Transform _firstPage;
        [SerializeField]
        private Transform _secondPage;

        public override void Feed(Character character)
        {
            _health.text = FormatAbsoluteStat(character.BaseHealth.MaxValue, character.BonusHealth.MaxValue);
            _mana.text = FormatAbsoluteStat(character.BaseMana.MaxValue, character.BonusMana.MaxValue);
            _essence.text = FormatAbsoluteStat(character.BaseEssence.MaxValue, character.BonusEssence.MaxValue);
            _strength.text = FormatAbsoluteStat(character.BaseStrength, character.BonusStrength);
            _defense.text = FormatAbsoluteStat(character.BaseDefense, character.BonusDefense);
            _magic.text = FormatAbsoluteStat(character.BaseMagic, character.BonusMagic);
            _magicDefense.text = FormatAbsoluteStat(character.BaseMagicDefense, character.BonusMagicDefense);
            _agility.text = FormatAbsoluteStat(character.BaseAgility, character.BonusAgility);
            _luck.text = FormatAbsoluteStat(character.BaseLuck, character.BonusLuck);

            _critChance.text = FormatRelativeStat(character.CritChance, 0);
            _evasion.text = FormatRelativeStat(character.Evasion, 0);
            _parry.text = FormatRelativeStat(character.Parry, 0);
            _provocation.text = FormatRelativeStat(character.Provocation, 0);
            _critDamage.text = FormatRelativeStat(character.CritDamage, 0);
            _accuracy.text = FormatRelativeStat(character.Accuracy, 0);

            if(_firstPage != null)
                _firstPage.gameObject.SetActive(true);

            if(_secondPage != null)
                _secondPage.gameObject.SetActive(false);
        }

        private static string FormatAbsoluteStat(int baseValue, int bonus)
        {
            if (bonus == 0)
                return baseValue.ToString();

            int sum = baseValue + bonus;

            return sum + FormatDetails(baseValue, bonus);
        }

        private static string FormatRelativeStat(int baseStat, int bonus)
        {
            if (bonus == 0)
                return baseStat + "%";

            int sum = baseStat + bonus;

            return sum + "%" + FormatDetails(baseStat, bonus);
        }

        private static string FormatDetails(int baseValue, int bonus)
        {
            string color = "green";
            string sign = "+";
            if (bonus < 0)
            {
                color = "red";
                sign = "-";
            }


            return " (" + baseValue + sign + "<color=\"" + color + "\">" + bonus + ")";
        }

        public void ChangePage()
        {
            if(_firstPage != null)
                _firstPage.gameObject.SetActive(!_firstPage.gameObject.activeSelf);

            if(_secondPage != null)
                _secondPage.gameObject.SetActive(!_secondPage.gameObject.activeSelf);
        }
    }
}
