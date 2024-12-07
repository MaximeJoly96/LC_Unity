using UnityEngine;
using TMPro;

namespace BattleSystem.UI
{
    public class SimpleTextWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public Animator Animator { get { return GetComponent<Animator>(); } }
        public TMP_Text Text { get { return _text; } set { _text = value; } }

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
