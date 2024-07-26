using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Behaviours
{
    public class AiScript
    {
        public List<AiScriptComponent> Components { get; private set; }
        public System.Random Rng { get; private set; }

        public AiScript()
        {
            Rng = new System.Random();
            Components = new List<AiScriptComponent>();
        }

        public void AddComponent(AiScriptComponent component)
        {
            Components.Add(component);
        }

        public int PickAction()
        {
            // The actions are picked based on the following information:
            // - each condition has a priority
            // - if two conditions have the same priority, then one is selected at random
            // - if priority N has no condition, we move to priority N-1 and check if there is a condition

            // First we order by priority descending, because we want to pick the one with the highest priority
            Components = Components.OrderByDescending(c => c.Priority).ToList();

            for(int i = 0; i < Components.Count; i++)
            {
                if (Components[i].Frequency == AiScriptComponent.BehaviourFrequency.Default)
                {
                    return Components[i].Abilities[Rng.Next(Components[i].Abilities.Count)];
                }
            }

            return -1;
        }
    }
}
