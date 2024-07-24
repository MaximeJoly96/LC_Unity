using UnityEngine;
using System.Collections.Generic;
using Abilities;
using Actors;
using Utils;

namespace BattleSystem.Model
{
    public class Battler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<ElementalAffinity> ElementalAffinities { get; set; }
        public List<ActiveEffect> ActiveEffects { get; set; }
        public List<Ability> Abilities { get; set; }
        public EssenceAffinity EssenceAffinity { get; set; }

        #region Stats
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

        #region Scaling
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

        public Battler()
        {
            Abilities = new List<Ability>();
            ElementalAffinities = new List<ElementalAffinity>();
            ActiveEffects = new List<ActiveEffect>();

            InitBasicAffinities();
        }

        public Battler(int id, string name, int level,
                       StatScalingFunction health,
                       StatScalingFunction mana,
                       StatScalingFunction essence,
                       StatScalingFunction strength,
                       StatScalingFunction defense,
                       StatScalingFunction magic,
                       StatScalingFunction magicDefense,
                       StatScalingFunction agility,
                       StatScalingFunction luck) : this()
        {
            Id = id;
            Name = name;
            Level = level;

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

        public Battler(Character character) : this(character.Id, character.Name, character.Level,
                                                   character.MaxHealthFunction,
                                                   character.MaxManaFunction,
                                                   character.MaxEssenceFunction,
                                                   character.StrengthFunction,
                                                   character.DefenseFunction,
                                                   character.MagicFunction,
                                                   character.MagicDefenseFunction,
                                                   character.AgilityFunction,
                                                   character.LuckFunction)
        {

        }

        private void InitBasicAffinities()
        {
            ElementalAffinities.Add(new ElementalAffinity(Element.Neutral, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Fire, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Ice, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Thunder, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Water, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Earth, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Wind, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Holy, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Darkness, 1.0f));
            ElementalAffinities.Add(new ElementalAffinity(Element.Healing, 1.0f));
        }
    }
}
