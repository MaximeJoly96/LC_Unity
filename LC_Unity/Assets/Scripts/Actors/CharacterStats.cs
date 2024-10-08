using Actors.Equipment;
using Effects;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Actors
{
    public class CharacterStats
    {
        private int _currentHealth;
        private int _currentMana;
        private int _currentEssence;
        private CharacterEquipment _equipment;

        #region Properties
        public int Exp { get; private set; }

        public int Level
        {
            get
            {
                float discriminant = ExpFunction.B * ExpFunction.B - 4 * ExpFunction.A * (ExpFunction.C - Exp);
                return Mathf.FloorToInt((-ExpFunction.B + Mathf.Sqrt(discriminant)) / (2 * ExpFunction.A));
            }
        }

        public int BaseStrength
        {
            get
            {
                return Mathf.RoundToInt(StrengthFunction.Compute(Level));
            }
        }

        public int BonusStrength
        {
            get
            {
                return GetStatFromItems("Strength") +
                       Mathf.RoundToInt((BaseStrength + GetStatFromItems("Strength")) * GetStatMultiplierFromItemEffects(Stat.Strength));
            }
        }

        public int BaseDefense
        {
            get
            {
                return Mathf.RoundToInt(DefenseFunction.Compute(Level));
            }
        }

        public int BonusDefense
        {
            get
            {
                return GetStatFromItems("Defense") +
                       Mathf.RoundToInt((BaseDefense + GetStatFromItems("Defense")) * GetStatMultiplierFromItemEffects(Stat.Defense));
            }
        }

        public int BaseMagic
        {
            get
            {
                return Mathf.RoundToInt(MagicFunction.Compute(Level));
            }
        }

        public int BonusMagic
        {
            get
            {
                return GetStatFromItems("Magic") +
                       Mathf.RoundToInt((BaseMagic + GetStatFromItems("Magic")) * GetStatMultiplierFromItemEffects(Stat.Magic));
            }
        }

        public int BaseMagicDefense
        {
            get
            {
                return Mathf.RoundToInt(MagicDefenseFunction.Compute(Level));
            }
        }

        public int BonusMagicDefense
        {
            get
            {
                return GetStatFromItems("MagicDefense") +
                       Mathf.RoundToInt((BaseMagicDefense + GetStatFromItems("MagicDefense")) * GetStatMultiplierFromItemEffects(Stat.MagicDefense));
            }
        }

        public int BaseAgility
        {
            get
            {
                return Mathf.RoundToInt(AgilityFunction.Compute(Level));
            }
        }

        public int BonusAgility
        {
            get
            {
                return GetStatFromItems("Agility") +
                       Mathf.RoundToInt((BaseAgility + GetStatFromItems("Agility")) * GetStatMultiplierFromItemEffects(Stat.Agility)); ;
            }
        }

        public int BaseLuck
        {
            get
            {
                return Mathf.RoundToInt(LuckFunction.Compute(Level));
            }
        }

        public int BonusLuck
        {
            get
            {
                return GetStatFromItems("Luck") +
                       Mathf.RoundToInt((BaseLuck + GetStatFromItems("Luck")) * GetStatMultiplierFromItemEffects(Stat.Luck)); ;
            }
        }

        public int CritChance
        {
            get
            {
                return Mathf.Clamp(5 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.CritChance) * 100), 0, 100);
            }
        }

        public int CritDamage
        {
            get
            {
                return 100 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.CritDmg) * 100); ;
            }
        }

        public int Parry
        {
            get
            {
                return Mathf.Clamp(10 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Parry) * 100), 0, 100);
            }
        }

        public int Evasion
        {
            get
            {
                return Mathf.Clamp(5 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Evasion) * 100), 0, 100);
            }
        }

        public int Provocation
        {
            get
            {
                return Mathf.Clamp(100 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Provocation) * 100), 0, 100);
            }
        }

        public int Accuracy
        {
            get
            {
                return Mathf.Clamp(90 + Mathf.RoundToInt(GetStatMultiplierFromItemEffects(Stat.Accuracy) * 100), 0, 100);
            }
        }

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Min(Mathf.Max(0, value), MaxHealth);
            }
        }

        public int BaseHealth
        {
            get
            {
                return Mathf.RoundToInt(MaxHealthFunction.Compute(Level));
            }
        }

        public int BonusHealth
        {
            get
            {
                return GetStatFromItems("Health") +
                       Mathf.RoundToInt((BaseHealth + GetStatFromItems("Health")) * GetStatMultiplierFromItemEffects(Stat.HP));
            }
        }

        public int MaxHealth { get { return BaseHealth + BonusHealth; } }

        public int CurrentMana
        {
            get { return _currentMana; }
            set
            {
                _currentMana = Mathf.Min(Mathf.Max(0, value), MaxMana);
            }
        }

        public int BaseMana
        {
            get
            {
                return Mathf.RoundToInt(MaxManaFunction.Compute(Level));
            }
        }

        public int BonusMana
        {
            get
            {
                return GetStatFromItems("Mana") +
                       Mathf.RoundToInt((BaseMana + GetStatFromItems("Mana")) * GetStatMultiplierFromItemEffects(Stat.MP));
            }
        }

        public int MaxMana { get { return BaseMana + BonusMana; } }

        public int CurrentEssence
        {
            get { return _currentEssence; }
            set
            {
                _currentEssence = Mathf.Min(Mathf.Max(0, value), MaxEssence);
            }
        }

        public int BaseEssence
        {
            get
            {
                return Mathf.RoundToInt(MaxEssenceFunction.Compute(Level));
            }
        }

        public int BonusEssence
        {
            get
            {
                return GetStatFromItems("Essence") +
                       Mathf.RoundToInt((BaseEssence + GetStatFromItems("Essence")) * GetStatMultiplierFromItemEffects(Stat.EP));
            }
        }

        public int MaxEssence { get { return BaseEssence + BonusEssence; } }

        public QuadraticFunction ExpFunction { get; private set; }
        public StatScalingFunction StrengthFunction { get; private set; }
        public StatScalingFunction DefenseFunction { get; private set; }
        public StatScalingFunction MagicFunction { get; private set; }
        public StatScalingFunction MagicDefenseFunction { get; private set; }
        public StatScalingFunction AgilityFunction { get; private set; }
        public StatScalingFunction LuckFunction { get; private set; }
        public StatScalingFunction MaxHealthFunction { get; private set; }
        public StatScalingFunction MaxManaFunction { get; private set; }
        public StatScalingFunction MaxEssenceFunction { get; private set; }
        #endregion

        public CharacterStats(QuadraticFunction expFunction,
                              StatScalingFunction strengthFunction,
                              StatScalingFunction defenseFunction,
                              StatScalingFunction magicFunction,
                              StatScalingFunction magicDefenseFunction,
                              StatScalingFunction agilityFunction,
                              StatScalingFunction luckFunction,
                              StatScalingFunction maxHealthFunction,
                              StatScalingFunction maxManaFunction,
                              StatScalingFunction maxEssenceFunction,
                              CharacterEquipment equipment)
        {
            Exp = 10;
            _equipment = equipment;

            ExpFunction = expFunction;
            StrengthFunction = strengthFunction;
            DefenseFunction = defenseFunction;
            MagicFunction = magicFunction;
            MagicDefenseFunction = magicDefenseFunction;
            AgilityFunction = agilityFunction;
            LuckFunction = luckFunction;
            MaxHealthFunction = maxHealthFunction;
            MaxManaFunction = maxManaFunction;
            MaxEssenceFunction = maxEssenceFunction;

            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;
            CurrentEssence = MaxEssence;
        }

        #region Methods
        public void GiveExperience(int amount)
        {
            Exp += amount;
        }

        public void SetExperience(int amount)
        {
            Exp = amount;
        }

        public int GetXpForCurrentLevel()
        {
            int requiredXp = GetTotalRequiredXpForLevel(Level);
            return Exp - requiredXp;
        }

        public int GetTotalRequiredXpForLevel(int level)
        {
            return Mathf.FloorToInt(ExpFunction.Compute(level));
        }

        public void ChangeHealth(int amount)
        {
            CurrentHealth -= amount;
        }

        public void ChangeMana(int amount)
        {
            CurrentMana -= amount;
        }

        public void ChangeEssence(int amount)
        {
            CurrentEssence -= amount;
        }

        private int GetStatFromItems(string propertyName)
        {
            int stat = 0;

            EquipmentItem rightHand = _equipment.RightHand.GetItem();
            if (rightHand != null && rightHand.Stats != null)
                stat += (int)rightHand.Stats.GetType().GetProperty(propertyName).GetValue(rightHand.Stats, null);

            EquipmentItem leftHand = _equipment.LeftHand.GetItem();
            if (leftHand != null && leftHand.Stats != null)
                stat += (int)leftHand.Stats.GetType().GetProperty(propertyName).GetValue(leftHand.Stats, null);

            EquipmentItem head = _equipment.Head.GetItem();
            if (head != null && head.Stats != null)
                stat += (int)head.Stats.GetType().GetProperty(propertyName).GetValue(head.Stats, null);

            EquipmentItem body = _equipment.Body.GetItem();
            if (body != null && body.Stats != null)
                stat += (int)body.Stats.GetType().GetProperty(propertyName).GetValue(body.Stats, null);

            EquipmentItem accessory = _equipment.Accessory.GetItem();
            if (accessory != null && accessory.Stats != null)
                stat += (int)accessory.Stats.GetType().GetProperty(propertyName).GetValue(accessory.Stats, null);

            return stat;
        }

        private float GetStatMultiplierFromItemEffects(Stat stat)
        {
            float multiplier = 0.0f;
            List<IEffect> allEffects = _equipment.GetAllItemEffects();

            for (int i = 0; i < allEffects.Count; i++)
            {
                if (allEffects[i] is StatBoost && (allEffects[i] as StatBoost).Stat == stat)
                    multiplier += (allEffects[i] as StatBoost).Value;
            }

            return multiplier / 100.0f;
        }
        #endregion
    }
}
