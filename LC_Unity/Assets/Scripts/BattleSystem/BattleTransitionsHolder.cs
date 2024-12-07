using UnityEngine;

namespace BattleSystem
{
    public class BattleTransitionsHolder : MonoBehaviour
    {
        [SerializeField]
        private BattleTransition[] _transitions;

        public void PlayRandomTransition()
        {
            int index = Random.Range(0, _transitions.Length);

            Instantiate(_transitions[index], transform);
        }
    }
}
