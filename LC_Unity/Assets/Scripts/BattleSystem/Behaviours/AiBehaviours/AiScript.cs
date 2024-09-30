using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class AiScript
    {
        public BehaviourCondition MainCondition { get; private set; }
        public System.Random Rng { get; private set; }

        public AiScript()
        {
            Rng = new System.Random();
        }

        public void SetMainCondition(BehaviourCondition condition)
        {
            MainCondition = condition;
        }

        public int PickAction()
        {
            
            return -1;
        }
    }
}
