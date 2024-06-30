using System.Collections.Generic;

namespace Abilities
{
    public enum AbilityUsability
    {
        Always,
        BattleOnly,
        MenuOnly
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

        public Ability(int id, string name, string description, AbilityCost cost, AbilityUsability usability, int priority)
        {
            Id = id;
            Name = name;
            Description = description;
            Cost = cost;
            Usability = usability;
            Priority = priority;
            Steps = new List<AbilityStep>();
        }
    }
}
