﻿using UnityEngine;
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
            _health.text = FormatAbsoluteStat(character.Stats.BaseHealth, character.Stats.BonusHealth);
            _mana.text = FormatAbsoluteStat(character.Stats.BaseMana, character.Stats.BonusMana);
            _essence.text = FormatAbsoluteStat(character.Stats.BaseEssence, character.Stats.BonusEssence);
            _strength.text = FormatAbsoluteStat(character.Stats.BaseStrength, character.Stats.BonusStrength);
            _defense.text = FormatAbsoluteStat(character.Stats.BaseDefense, character.Stats.BonusDefense);
            _magic.text = FormatAbsoluteStat(character.Stats.BaseMagic, character.Stats.BonusMagic);
            _magicDefense.text = FormatAbsoluteStat(character.Stats.BaseMagicDefense, character.Stats.BonusMagicDefense);
            _agility.text = FormatAbsoluteStat(character.Stats.BaseAgility, character.Stats.BonusAgility);
            _luck.text = FormatAbsoluteStat(character.Stats.BaseLuck, character.Stats.BonusLuck);

            _critChance.text = FormatRelativeStat(character.Stats.CritChance, 0);
            _evasion.text = FormatRelativeStat(character.Stats.Evasion, 0);
            _parry.text = FormatRelativeStat(character.Stats.Parry, 0);
            _provocation.text = FormatRelativeStat(character.Stats.Provocation, 0);
            _critDamage.text = FormatRelativeStat(character.Stats.CritDamage, 0);
            _accuracy.text = FormatRelativeStat(character.Stats.Accuracy, 0);

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
