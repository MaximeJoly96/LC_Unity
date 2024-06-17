using UnityEngine;

namespace Field
{
    public class FieldBuilder : MonoBehaviour
    {
        [SerializeField]
        private PlayableField _field;

        private void Awake()
        {
            BuildField(_field);
        }

        public void BuildField(PlayableField field)
        {
            Instantiate(field.gameObject);
        }
    }
}
