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
                    foreach(Transform mt in _transitionsWrapper)
                    {
                        _transitions.Add(mt.GetComponent<MapTransition>());
                    }
                }

                return _transitions;
            }
        }

        public string BgmKey { get { return _bgmKey; } }

        public void DisableCollisions(bool disable)
        {
            foreach(Transform col in _collisions)
            {
                col.gameObject.SetActive(!disable);
            }
        }
    }
}
