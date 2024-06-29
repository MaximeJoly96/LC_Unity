using UnityEngine;
using TMPro;

namespace BattleSystem.UI
{
    public class SimpleTextWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private Animator Animator { get { return GetComponent<Animator>(); } }

        public void UpdateText(string text)
        {
            _text.text = text;
        }

        public void Show()
        {
            Animator.Play("ShowSimpleWindow");
        }

        public void Hide()
        {
            Animator.Play("HideSimpleWindow");
        }
    }
}
