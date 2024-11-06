using UnityEngine;

namespace Field
{
    public class SpriteOverlapHandler : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _renderer.sortingOrder = Mathf.RoundToInt(_renderer.bounds.min.y * -20.0f);
        }
    }
}
