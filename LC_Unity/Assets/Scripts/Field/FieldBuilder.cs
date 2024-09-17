using UnityEngine;
using Logging;
using Save;
using Movement;
using Timing;
using Core;
using Shop;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
using MusicAndSounds;

namespace Field
{
    public class FieldBuilder : MonoBehaviour
    {
        [SerializeField]
        private PlayableField[] _allFields;
        [SerializeField]
        private GameObject _interiorMask;

        private List<PlayableField> _instFields;
        private PlayableField _currentField;
        protected Door[] _doors;

        public PlayableField CurrentField
        {
            get { return _currentField; }
            private set
            {
                _currentField = value;
                GlobalStateMachine.Instance.CurrentMapId = _currentField.MapId;
                MapNameDisplay display = FindObjectOfType<MapNameDisplay>();

                if (display)
                    display.Show();
            }
        }

        private void Awake()
        {
            FindObjectOfType<GlobalTimer>().Running = true;

            _instFields = new List<PlayableField>();
            BuildField(_allFields.FirstOrDefault(f => f.MapId == GlobalStateMachine.Instance.CurrentMapId));

            ScanForAgents();
            ScanForDoors();
            ScanForTransitions();

            PositionPlayer();
            PlayFieldBgm(CurrentField);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
        }

        public void BuildField(PlayableField field)
        {
            PlayableField mainField = Instantiate(field);
            CurrentField = mainField;
            _instFields.Add(mainField);

            FindObjectOfType<ShopManager>().LoadMerchants(mainField.Merchants);

            for(int i = 0; i < field.NeighbourFields.Length; i++)
            {
                _instFields.Add(Instantiate(field.NeighbourFields[i]));
            }
        }

        public void ScanForAgents()
        {
            if(!CurrentField)
            {
                LogsHandler.Instance.LogError("Cannot scan for agents if no playable field has been created.");
                return;
            }

            Agent[] agents = CurrentField.transform.GetComponentsInChildren<Agent>(true);

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
            CurrentField.DisableCollisions(switchOn);
        }

        private void PositionPlayer()
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            if(pc)
                pc.transform.position = SaveManager.Instance.Data.PlayerPosition;
        }

        private void ScanForTransitions()
        {
            for(int i = 0; i < _instFields.Count; i++)
            {
                for (int j = 0; j < _instFields[i].Transitions.Count; j++)
                {
                    _instFields[i].Transitions[j].TransitionnedToMap.AddListener(TransitionOccured);
                }
            }
        }

        private void TransitionOccured(int mapId)
        {
            if(CurrentField.MapId != mapId)
            { 
                PlayableField newField = _instFields.FirstOrDefault(f => f.MapId == mapId);

                if (newField.BgmKey != CurrentField.BgmKey)
                {
                    StopCurrentBgm(CurrentField);
                    PlayFieldBgm(newField);
                }

                CurrentField = newField;

                for(int i = 0; i < CurrentField.NeighbourFields.Length; i++)
                {
                    if(_instFields.FirstOrDefault(f => CurrentField.NeighbourFields[i].MapId == f.MapId) == null)
                    {
                        _instFields.Add(Instantiate(CurrentField.NeighbourFields[i]));
                    }
                }

                ScanForTransitions();
                DestroyUnnecessaryFields();
            }
        }

        private void DestroyUnnecessaryFields()
        {
            _instFields = _instFields.Where(f => f != null).ToList();
            List<PlayableField> toDestroy = new List<PlayableField>();

            for(int i = 0; i < _instFields.Count; i++)
            {
                if (_instFields[i].MapId != CurrentField.MapId && 
                    !CurrentField.NeighbourFields.FirstOrDefault(f => f.MapId == _instFields[i].MapId))
                    toDestroy.Add(_instFields[i]);
            }

            for (int i = 0; i < toDestroy.Count; i++)
                Destroy(toDestroy[i].gameObject);
        }

        private void PlayFieldBgm(PlayableField field)
        {
            Engine.MusicAndSounds.PlayBgm play = new Engine.MusicAndSounds.PlayBgm
            {
                Name = field.BgmKey,
                Volume = 1.0f,
                Pitch = 1.0f
            };
            FindObjectOfType<AudioPlayer>().PlayBgm(play);
        }

        private void StopCurrentBgm(PlayableField field)
        {
            Engine.MusicAndSounds.FadeOutBgm fade = new Engine.MusicAndSounds.FadeOutBgm
            {
                Name = field.BgmKey,
                TransitionDuration = 0.2f,
            };
            FindObjectOfType<AudioPlayer>().FadeOutBgm(fade);
        }
    }
}
