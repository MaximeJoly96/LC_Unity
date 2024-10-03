using Abilities;
using Actors;
using Effects;
using System;
using Core.Model;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class HasEnoughResources : BehaviourCondition
    {
        public enum AmountType { Flat, FromAbility, Percentage }

        public AmountType Amount { get; private set; }
        public float Value { get; private set; }
        public Stat Resource { get; private set; } 

        public HasEnoughResources(AmountType amount, float value, Stat resource)
        {
            Amount = amount;
            Resource = resource;
            Value = value;
        }

        public bool Check(Character character, Ability ability)
        {
            switch(Amount)
            {
                case AmountType.Flat:
                    return GetCurrentStat(Resource, character) >= Value;
                case AmountType.FromAbility:
                    return GetCurrentStat(Resource, character) >= GetSpecificCostForAbility(Resource, ability);
                case AmountType.Percentage:
                    float currentPercentage = GetCurrentStat(Resource, character) / (float)GetMaxStat(Resource, character) * 100.0f;
                    return currentPercentage >= Value;
            }

            return false;
        }

        public bool Check(Character character)
        {
            if (Amount == AmountType.FromAbility)
                return Check(character, AbilitiesManager.Instance.GetAbility((int)Value));

            return Check(character, new Ability(new ElementIdentifier(0, "", ""), 0, AbilityUsability.Always, TargetEligibility.Any, AbilityCategory.Skill, 0));
        }

        private int GetCurrentStat(Stat stat, Character character)
        {
            switch(stat)
            {
                case Stat.HP:
                    return character.CurrentHealth;
                case Stat.MP:
                    return character.CurrentMana;
                case Stat.EP:
                    return character.CurrentEssence;
                default:
                    throw new InvalidOperationException("Invalid stat used as Resource (" + stat + "). You can only use HP, MP and EP.");
            }
        }

        private int GetMaxStat(Stat stat, Character character)
        {
            switch(stat)
            {
                case Stat.HP:
                    return character.MaxHealth;
                case Stat.MP:
                    return character.MaxMana;
                case Stat.EP:
                    return character.MaxEssence;
                default:
                    throw new InvalidOperationException("Invalid stat used as Resource (" + stat + "). You can only use HP, MP and EP.");
            }
        }

        private int GetSpecificCostForAbility(Stat stat, Ability ability)
        {
            switch(stat)
            {
                case Stat.HP:
                    return ability.Cost.HealthCost;
                case Stat.MP:
                    return ability.Cost.ManaCost;
                case Stat.EP:
                    return ability.Cost.EssenceCost;
                default:
                    throw new InvalidOperationException("Such cost (" + stat.ToString() + ") does not exist for abilities.");
            }
        }
    }
}
