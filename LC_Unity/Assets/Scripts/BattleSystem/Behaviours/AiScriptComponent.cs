using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Behaviours
{
    public class AiScriptComponent
    {
        public enum BehaviourFrequency { Default }

        public BehaviourFrequency Frequency { get; set; }
        public List<int> Abilities { get; private set; }
        public int Priority { get; set; }
        
        public AiScriptComponent()
        {
            Abilities = new List<int>();
        }

        public void AddAbility(int id)
        {
            Abilities.Add(id);
        }
    }
}
