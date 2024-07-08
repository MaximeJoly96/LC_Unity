using UnityEngine;
using Logging;

namespace Field
{
    public class FieldBuilder : MonoBehaviour
    {
        [SerializeField]
        private PlayableField _field;
        [SerializeField]
        private GameObject _interiorMask;

        private PlayableField _instField;

        protected Door[] _doors;

        private void Awake()
        {
            BuildField(_field);
            ScanForAgents();
            ScanForDoors();
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

        protected virtual void ScanForDoors()
        {
            _doors = FindObjectsOfType<Door>();

            foreach (Door door in _doors)
            {
                door.DoorStatusChanged.AddListener(SwitchToInteriorMode);
            }
        }

        public virtual void SwitchToInteriorMode(bool switchOn)
        {
            _interiorMask.SetActive(switchOn);
            _instField.DisableCollisions(switchOn);
        }
    }
}
