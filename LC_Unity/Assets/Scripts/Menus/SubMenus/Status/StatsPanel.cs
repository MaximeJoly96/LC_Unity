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

            _critChance.text = FormatRelativeStat(0, 0);
            _evasion.text = FormatRelativeStat(0, 0);
            _parry.text = FormatRelativeStat(0, 0);
            _provocation.text = FormatAbsoluteStat(0, 0);
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
    }
}
