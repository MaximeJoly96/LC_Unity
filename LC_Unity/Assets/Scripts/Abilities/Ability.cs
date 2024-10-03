using System.Collections.Generic;
using BattleSystem;
using Effects;
using UnityEngine;
using Core.Model;

namespace Abilities
{
    public enum AbilityUsability
    {
        Always,
        BattleOnly,
        MenuOnly
    }

    public enum TargetEligibility
    {
        Self,
        Enemy,
        Ally,
        All,
        AllAllies,
        AllEnemies,
        Any,
        AnyExceptSelf
    }

    public enum AbilityCategory
    {
        AttackCommand,
        Skill,
        FleeCommand
    }

    public class Ability
    {
        private ElementIdentifier _identifier;
        
        public AbilityCost Cost { get; private set; }
        public AbilityUsability Usability { get; private set; }
        public int Priority { get; private set; }
        public List<BattlerBehaviour> Targets { get; set; }
        public TargetEligibility TargetEligibility { get; set; }
        public AbilityCategory Category { get; set; }
        public int Range { get; set; }
        public AbilityAnimation Animation { get; set; }
        public List<IEffect> Effects { get; set; }
        public float AnimationLength
        {
            get
            {
                AttackAnimationsWrapper animationsWrapper = Object.FindObjectOfType<AttackAnimationsWrapper>();
                if (!animationsWrapper)
                    return 0.0f;

                return 0.0f; 
            }
        }
        public int Id { get { return _identifier.Id; } }
        public string Name { get { return _identifier.NameKey; } }
        public string Description { get { return _identifier.DescriptionKey; } }

        public Ability(ElementIdentifier identifier, int priority, AbilityUsability usability, 
                       TargetEligibility eligibility, AbilityCategory category, int range)
        {
            _identifier = identifier;
            Priority = priority;
            Usability = usability;
            TargetEligibility = eligibility;
            Category = category;
            Range = range;

            Effects = new List<IEffect>();
        }

        public Ability(Ability ability) : this(ability._identifier, ability.Priority, 
                                               ability.Usability, ability.TargetEligibility, 
                                               ability.Category, ability.Range)
        {

        }

        public void SetCost(int hp, int mp, int ep)
        {
            SetCost(new AbilityCost(hp, mp, ep));
        }
        public void SetCost(AbilityCost cost)
        {
            Cost = cost;
        }

        public void SetAnimation(AbilityAnimation animation)
        {
            Animation = animation;
        }

        public void SetAnimation(string channelName, string strikeName, int channelParticlesId, int impactParticlesId, int projectileId)
        {
            SetAnimation(new AbilityAnimation(channelName, strikeName, channelParticlesId, impactParticlesId, projectileId));
        }

        public void SetEffects(IEnumerable<IEffect> effects)
        {
            foreach(IEffect effect in effects)
            {
                Effects.Add(effect);
            }
        }
    }
}
