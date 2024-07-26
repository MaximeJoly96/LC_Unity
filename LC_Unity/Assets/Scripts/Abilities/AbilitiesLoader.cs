using UnityEngine;

namespace Abilities
{
    public class AbilitiesLoader : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _abilities;

        private void Awake()
        {
            AbilitiesManager.Instance.Init(_abilities);
        }
    }
}
