using UnityEngine;

namespace Field
{
    public class PlayableField : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _collisions;

        public void DisableCollisions(bool disable)
        {
            foreach(Transform col in _collisions)
            {
                col.gameObject.SetActive(!disable);
            }
        }
    }
}
