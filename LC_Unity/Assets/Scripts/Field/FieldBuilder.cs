using UnityEngine;
using Logging;
using Save;
using Movement;
using Timing;
using Core;
using Shop;

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
            FindObjectOfType<GlobalTimer>().Running = true;

            BuildField(_field);
            ScanForAgents();
            ScanForDoors();

            PositionPlayer();
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
        }

        public void BuildField(PlayableField field)
        {
            _instField = Instantiate(field);
            FindObjectOfType<ShopManager>().LoadMerchants(_instField.Merchants);
        }

        public void ScanForAgents()
        {
            if(!_instField)
            {
                LogsHandler.Instance.LogError("Cannot scan for agents if no playable field has been created.");
                return;
            }

            Agent[] agents = _instField.transform.GetComponentsInChildren<Agent>(true);

            AgentsManager.Instance.Reset();

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

        private void PositionPlayer()
        {
            FindObjectOfType<PlayerController>().transform.position = SaveManager.Instance.Data.PlayerPosition;
        }
    }
}
