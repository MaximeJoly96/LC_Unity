using UnityEngine;
using Logging;

namespace Field
{
    public class FieldBuilder : MonoBehaviour
    {
        [SerializeField]
        private PlayableField _field;

        private PlayableField _instField;

        private void Awake()
        {
            BuildField(_field);
            ScanForAgents();
        }

        public void BuildField(PlayableField field)
        {
            _instField = Instantiate(field);
        }

        public void ScanForAgents()
        {
            if(!_instField)
            {
                LogsHandler.Instance.LogError("Cannot scan for agents if no playable field has been created.");
                return;
            }

            Agent[] agents = _instField.transform.GetComponentsInChildren<Agent>(true);

            for(int i = 0; i < agents.Length; i++)
            {
                AgentsManager.Instance.RegisterAgent(agents[i]);
            }
        }
    }
}
