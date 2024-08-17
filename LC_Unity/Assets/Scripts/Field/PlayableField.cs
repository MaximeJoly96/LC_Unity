using UnityEngine;

namespace Field
{
    public class PlayableField : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _collisions;
        [SerializeField]
        private TextAsset _merchants;

        public TextAsset Merchants {  get { return _merchants; } }

        public void DisableCollisions(bool disable)
        {
            foreach(Transform col in _collisions)
            {
                col.gameObject.SetActive(!disable);
            }
        }
    }
}
