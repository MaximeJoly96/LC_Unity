using Logging;
using Utils;
using UnityEngine;

namespace Actors
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Exp { get; private set; }

        #region Stats
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

        public int BaseDefense
        {
            get
            {
                return Mathf.RoundToInt(DefenseFunction.Compute(Level));
            }
        }

        public int BaseMagic
        {
            get
            {
                return Mathf.RoundToInt(MagicFunction.Compute(Level));
            }
        }

        public int BaseMagicDefense
        {
            get
            {
                return Mathf.RoundToInt(MagicDefenseFunction.Compute(Level));
            }
        }

        public int BaseAgility
        {
            get
            {
                return Mathf.RoundToInt(AgilityFunction.Compute(Level));
            }
        }

        public int BaseLuck
        {
            get
            {
                return Mathf.RoundToInt(LuckFunction.Compute(Level));
            }
        }

        public Resource BaseHealth
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxHealthFunction.Compute(Level)));
            }
        }

        public Resource BaseMana
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxManaFunction.Compute(Level)));
            }
        }

        public Resource BaseEssence
        {
            get
            {
                return new Resource(Mathf.RoundToInt(MaxEssenceFunction.Compute(Level)));
            }
        }
        #endregion

        #region Scaling Functions
        public QuadraticFunction ExpFunction { get; set; }
        public StatScalingFunction StrengthFunction { get; set; }
        public StatScalingFunction DefenseFunction { get; set; }
        public StatScalingFunction MagicFunction { get; set; }
        public StatScalingFunction MagicDefenseFunction { get; set; }
        public StatScalingFunction AgilityFunction { get; set; }
        public StatScalingFunction LuckFunction { get; set; }
        public StatScalingFunction MaxHealthFunction { get; set; }
        public StatScalingFunction MaxManaFunction { get; set; }
        public StatScalingFunction MaxEssenceFunction { get; set; }
        #endregion

        public Character()
        {

        }

        public Character(int id, string name, 
                         QuadraticFunction exp, 
                         StatScalingFunction health,
                         StatScalingFunction mana,
                         StatScalingFunction essence,
                         StatScalingFunction strength,
                         StatScalingFunction defense,
                         StatScalingFunction magic,
                         StatScalingFunction magicDefense,
                         StatScalingFunction agility,
                         StatScalingFunction luck)
        {
            Id = id;
            Name = name;
            Exp = 10;

            ExpFunction = exp;
            MaxHealthFunction = health;
            MaxManaFunction = mana;
            MaxEssenceFunction = essence;
            StrengthFunction = strength;
            DefenseFunction = defense;
            MagicFunction = magic;
            MagicDefenseFunction = magicDefense;
            AgilityFunction = agility;
            LuckFunction = luck;
        }

        public void ChangeLevel(int amount)
        {
            LogsHandler.Instance.LogWarning("ChangeLevel has not been implemented yet.");
        }

        public void GiveExp(int amount)
        {
            Exp += amount;
        }

        public void Recover()
        {
            LogsHandler.Instance.LogWarning("Recover has not been implemented yet.");
        }

        public void ChangeEquipment(int itemId)
        {
            LogsHandler.Instance.LogWarning("ChangeEquipment has not been implemented yet.");
        }

        public void LearnSkill(int skillId)
        {
            LogsHandler.Instance.LogWarning("LearnSkill has not been implemented yet.");
        }

        public void ForgetSkill(int skillId)
        {
            LogsHandler.Instance.LogWarning("ForgetSkill has not been implemented yet.");
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

        public int GetXpRequiredForLevel(int level)
        {
            int required = GetTotalRequiredXpForLevel(level);
            int requiredNext = GetTotalRequiredXpForLevel(level + 1);

            return requiredNext - required;
        }
    }
}
