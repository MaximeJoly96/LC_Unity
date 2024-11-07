using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class PlayableField : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _collisions;
        [SerializeField]
        private TextAsset _merchants;
        [SerializeField]
        private PlayableField[] _neighbourFields;
        [SerializeField]
        private int _mapId;
        [SerializeField]
        private Transform _transitionsWrapper;
        [SerializeField]
        private string _bgmKey;
        [SerializeField]
        private bool _showOnlyIfCurrentMap;

        private List<MapTransition> _transitions;

        public TextAsset Merchants {  get { return _merchants; } }
        public PlayableField[] NeighbourFields { get { return _neighbourFields; } }
        public int MapId { get { return _mapId; } }
        public List<MapTransition> Transitions
        {
            get
            {
                if(_transitions == null)
                {
                    _transitions = new List<MapTransition>();

                    if(_transitionsWrapper != null)
                    {
                        foreach (Transform mt in _transitionsWrapper)
                        {
                            _transitions.Add(mt.GetComponent<MapTransition>());
                        }
                    }
                }

                return _transitions;
            }
        }

        public string BgmKey { get { return _bgmKey; } }
        public bool ShowOnlyIfCurrentMap { get { return _showOnlyIfCurrentMap;} }

        public void DisableCollisions(bool disable)
        {
            foreach(Transform col in _collisions)
            {
                col.gameObject.SetActive(!disable);
            }
        }

        private void Start()
        {
            if (_showOnlyIfCurrentMap)
                Show(GlobalStateMachine.Instance.CurrentMapId == _mapId);
            else
                Show(true);
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}
