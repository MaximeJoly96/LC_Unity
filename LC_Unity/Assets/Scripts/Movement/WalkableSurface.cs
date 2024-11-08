using UnityEngine;

namespace Movement
{
    public class WalkableSurface : MonoBehaviour
    {
        [SerializeField]
        private GroundType _groundType;

        public GroundType GroundType { get { return _groundType; } }
    }
}
