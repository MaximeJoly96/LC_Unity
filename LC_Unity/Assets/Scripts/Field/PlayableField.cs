using UnityEngine;

namespace Field
{
    public class PlayableField : MonoBehaviour
    {
        [SerializeField]
        private Sprite _groundLayer;
        [SerializeField]
        private Sprite _upperLayer;

        private void Awake()
        {
            CreateLayer(_groundLayer, false);
            CreateLayer(_upperLayer, true);
        }

        private void CreateLayer(Sprite layer, bool abovePlayer)
        {
            GameObject go = new GameObject("Layer");
            go.transform.SetParent(transform);

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = layer;

            sr.sortingLayerName = abovePlayer ? "AbovePlayer" : "BelowPlayer";        
        }
    }
}
