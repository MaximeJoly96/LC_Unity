using UnityEngine;
using TMPro;

namespace BattleSystem.UI
{
    public class MoveSelectionWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _instructionsText;
        [SerializeField]
        private Transform _cursor;
        [SerializeField]
        private Transform _listWrapper;

        private Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public void UpdateInstructions(string characterName)
        {
            _instructionsText.text = "Select " + characterName + "'s next move.";
        }

        public void Show()
        {
            Animator.Play("Show");
        }
    }
}
