using System.Collections.Generic;
using System.Linq;

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
    }
}
