using Engine.Character;
using Logging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field
{
    public class AgentsManager
    {
        private Dictionary<string, Agent> _registeredAgents;

        private static AgentsManager _instance;

        public static AgentsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AgentsManager();

                return _instance;
            }
        }

        private AgentsManager()
        {
            Reset();
        }

        public void RegisterAgent(Agent agent)
        {
            _registeredAgents.Add(agent.Id, agent);
        }

        public Agent GetAgent(string id)
        {
            return _registeredAgents[id];
        }

        public void Reset()
        {
            _registeredAgents = new Dictionary<string, Agent>();
        }

        public void ShowAgentAnimation(ShowAgentAnimation showAction)
        {
            Agent agent = GetAgent(showAction.Target);
            if (!agent)
            {
                LogsHandler.Instance.LogError("Agent " + showAction.Target + " does not exist, but you're trying to play one of its animations.");
                return;
            }

            agent.PlayAnimation(showAction.AnimationName);
        }

        public void ResetAgentAnimation(ResetAgentAnimationState resetAction)
        {
            Agent agent = GetAgent(resetAction.Target);
            if (!agent)
            {
                LogsHandler.Instance.LogError("Agent " + resetAction.Target + " does not exist, but you're trying to reset its animation state.");
                return;
            }

            agent.ResetAnimation();
        }

    }
}
