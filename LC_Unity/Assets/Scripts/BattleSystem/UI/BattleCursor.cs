using UnityEngine;

namespace BattleSystem.UI
{
    public class BattleCursor : MonoBehaviour
    {
        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void Stop()
        {
            Animator.Play("StopBattleCursor");
        }
    }
}
