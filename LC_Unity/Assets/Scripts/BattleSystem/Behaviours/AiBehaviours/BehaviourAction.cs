using Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class BehaviourAction
    {
        public List<int> Abilities { get; private set; }
        public BehaviourCondition Condition { get; private set; }

        public BehaviourAction()
        {
            Abilities = new List<int>();
        }

        public void AddAbility(int abilityId)
        {
            Abilities.Add(abilityId);
        }

        public void AddAbilities(IEnumerable<int> abilitiesId)
        {
            Abilities.AddRange(abilitiesId);
        }

        public void SetCondition(BehaviourCondition condition)
        {
            Condition = condition;
        }
    }
}
