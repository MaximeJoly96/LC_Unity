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
            _health.text = FormatAbsoluteStat(character.BaseHealth.MaxValue, 0);
            _mana.text = FormatAbsoluteStat(character.BaseMana.MaxValue, 0);
            _essence.text = FormatAbsoluteStat(character.BaseEssence.MaxValue, 0);
            _strength.text = FormatAbsoluteStat(character.BaseStrength, 0);
            _defense.text = FormatAbsoluteStat(character.BaseDefense, 0);
            _magic.text = FormatAbsoluteStat(character.BaseMagic, 0);
            _magicDefense.text = FormatAbsoluteStat(character.BaseMagicDefense, 0);
            _agility.text = FormatAbsoluteStat(character.BaseAgility, 0);
            _luck.text = FormatAbsoluteStat(character.BaseLuck, 0);

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


            return " (" + baseValue + " " + sign + "<color=\"" + color + "\">" + bonus + ")";
        }
    }
}
