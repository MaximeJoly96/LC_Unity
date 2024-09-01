using Movement;
using UnityEngine;
using UnityEngine.Events;

namespace Field
{
    public class MapTransition : MonoBehaviour
    {
        [SerializeField]
        private int _idToTransitionInto;

        private UnityEvent<int> _transitionnedToMap;

        public UnityEvent<int> TransitionnedToMap
        {
            get
            {
                if (_transitionnedToMap == null)
                    _transitionnedToMap = new UnityEvent<int>();

                return _transitionnedToMap;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            if(pc)
            {
                TransitionnedToMap.Invoke(_idToTransitionInto);
            }
        }
    }
}
