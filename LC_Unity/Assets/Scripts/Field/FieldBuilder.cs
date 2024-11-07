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
using Engine.Movement;
using UnityEngine.UIElements;
using System.Collections;

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
            ScanForTransitions();

            PositionPlayer();
            PlayFieldBgm(CurrentField);
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
        }

        public void BuildField(PlayableField field)
        {
            PlayableField mainField = _instFields.FirstOrDefault(f => f.MapId == field.MapId);
            if(!mainField)
            {
                mainField = Instantiate(field);
                _instFields.Add(mainField);
            }  

            mainField.gameObject.SetActive(true);
            CurrentField = mainField;

            FindObjectOfType<ShopManager>().LoadMerchants(mainField.Merchants);

            for(int i = 0; i < field.NeighbourFields.Length; i++)
            {
                if(!_instFields.Any(f => f.MapId == field.NeighbourFields[i].MapId))
                {
                    PlayableField neighbour = Instantiate(field.NeighbourFields[i]);
                    _instFields.Add(neighbour);

                    neighbour.gameObject.SetActive(!mainField.ShowOnlyIfCurrentMap);
                }  
            }

            if(mainField.ShowOnlyIfCurrentMap)
                SwitchToInteriorMode(true);
        }

        public void ScanForAgents()
        {
            if(!CurrentField)
            {
                LogsHandler.Instance.LogError("Cannot scan for agents if no playable field has been created.");
                return;
            }

            List<Agent> agents = CurrentField.transform.GetComponentsInChildren<Agent>(true).ToList();

            foreach (PlayableField neighbour in CurrentField.NeighbourFields)
                agents.AddRange(neighbour.transform.GetComponentsInChildren<Agent>(true));

            AgentsManager.Instance.Reset();

            for(int i = 0; i < agents.Count; i++)
            {
                AgentsManager.Instance.RegisterAgent(agents[i]);
            }

            AgentsManager.Instance.RegisterAgent(FindObjectOfType<PlayerController>().GetComponent<Agent>());
        }

        public void TransferObject(TransferObject transferObject)
        {
            if(transferObject.MapId != CurrentField.MapId)
            {
                PlayableField newField = _allFields.FirstOrDefault(f => f.MapId == transferObject.MapId);

                if (newField != null)
                {
                    Destroy(CurrentField.gameObject);
                    BuildField(newField);

                    ScanForAgents();
                    ScanForTransitions();
                }
            }

            if (transferObject.Target.ToLower() == "player")
            {
                SwitchToInteriorMode(transferObject.Inside);

                PositionPlayer(new Vector2(transferObject.X, transferObject.Y));
                ChangePlayerDirection(transferObject.Direction);
            }
            else
            {
                Agent agent = AgentsManager.Instance.GetAgent(transferObject.Target);
                if (agent != null)
                {
                    agent.transform.position = new Vector3(transferObject.X, transferObject.Y);
                    agent.UpdateDirection(transferObject.Direction);
                }
            }
        }

        public virtual void SwitchToInteriorMode(bool switchOn)
        {
            _interiorMask.SetActive(switchOn);
            CurrentField.DisableCollisions(switchOn);

            PlayerController pc = FindObjectOfType<PlayerController>();
            if (pc)
            {
                pc.GetComponent<SpriteRenderer>().sortingLayerName = switchOn ? "InteriorPlayer" : "Player";
                pc.transform.localScale = switchOn ? new Vector3(0.8f, 0.8f, 0.8f) : Vector3.one;
            }  
        }

        private void PositionPlayer()
        {
            PositionPlayer(SaveManager.Instance.Data.PlayerPosition);
        }

        private void PositionPlayer(Vector2 position)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            if (pc)
                pc.transform.position = position;
        }

        private void ChangePlayerDirection(TransferObject.PossibleDirection direction)
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            if (pc)
                pc.ForceDirection(direction);
        }

        private void ScanForTransitions()
        {
            for(int i = 0; i < _instFields.Count; i++)
            {
                for (int j = 0; j < _instFields[i].Transitions.Count; j++)
                {
                    _instFields[i].Transitions[j].TransitionnedToMap.RemoveAllListeners();
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
                    if (_instFields.Count(f => f.MapId == CurrentField.NeighbourFields[i].MapId) == 0)
                    {
                        _instFields.Add(Instantiate(CurrentField.NeighbourFields[i]));
                    }
                }

                ScanForAgents();
                ScanForTransitions();
                StartCoroutine(DestroyUnnecessaryFields());
            }
        }

        private IEnumerator DestroyUnnecessaryFields()
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

            yield return null;

            _instFields = _instFields.Where(f => f != null).ToList();
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
