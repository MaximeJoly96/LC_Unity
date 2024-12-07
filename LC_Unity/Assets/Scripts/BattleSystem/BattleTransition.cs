using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleSystem
{
    public class BattleTransition : MonoBehaviour
    {
        private void Awake()
        {
            StartTransition();
        }

        private void StartTransition()
        {
            GetComponent<Animator>().Play("PlayTransition");
        }

        public void TransitionFinished()
        {
            SceneManager.LoadSceneAsync("Battle");
        }
    }
}
