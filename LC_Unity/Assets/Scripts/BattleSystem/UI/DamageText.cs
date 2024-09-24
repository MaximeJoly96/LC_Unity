using TMPro;
using UnityEngine;

namespace BattleSystem.UI
{
    public class DamageText : MonoBehaviour
    {
        private TMP_Text _text;
        private Animator _animator;

        private TMP_Text Text
        {
            get
            {
                if(!_text)
                    _text = GetComponent<TMP_Text>();

                return _text;
            }
        }

        private Animator Animator
        {
            get
            {
                if(! _animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        public void Show(int value)
        {
            Text.text = value.ToString();
            Animator.Play("Show");
        }

        public void FinishedShowing()
        {
            Destroy(gameObject);
        }
    }
}
