using UnityEngine;

namespace Field
{
    public class SpriteOnSurface : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private SpriteRenderer _parent;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _parent = transform.parent.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _sr.sortingOrder = _parent.sortingOrder + 1;
        }
    }
}
