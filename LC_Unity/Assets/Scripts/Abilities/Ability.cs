using System.Collections.Generic;
using BattleSystem;

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
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AbilityCost Cost { get; private set; }
        public AbilityUsability Usability { get; private set; }
        public int Priority { get; private set; }
        public List<AbilityStep> Steps { get; private set; }
        public List<BattlerBehaviour> Targets { get; set; }
        public TargetEligibility TargetEligibility { get; set; }
        public AbilityCategory Category { get; set; }
        public int Range { get; set; } = 100;
        public int AnimationId { get; set; }

        public Ability(int id, string name, string description, AbilityCost cost, AbilityUsability usability, int priority, TargetEligibility targetEligibility, AbilityCategory category)
        {
            Id = id;
            Name = name;
            Description = description;
            Cost = cost;
            Usability = usability;
            Priority = priority;
            Steps = new List<AbilityStep>();
            TargetEligibility = targetEligibility;
            Category = category;
        }

        public Ability(Ability ability) : this(ability.Id, 
                                               ability.Name, 
                                               ability.Description, 
                                               ability.Cost, 
                                               ability.Usability, 
                                               ability.Priority, 
                                               ability.TargetEligibility, 
                                               ability.Category)
        {

        }
    }
}
