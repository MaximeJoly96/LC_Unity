using UnityEngine;

namespace BattleSystem.UI
{
    public class PlacementCursor : MonoBehaviour
    {
        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void StopAnimation()
        {
            Animator.Play("PlacementCursorStop");
        }
    }
}
