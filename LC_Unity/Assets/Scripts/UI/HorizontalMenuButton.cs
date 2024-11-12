using UnityEngine;

namespace UI
{
    public class HorizontalMenuButton : MonoBehaviour
    {
        #region Protected members
        protected Animator _animator;
        #endregion

        #region Properties
        protected Animator _Animator
        {
            get
            {
                if(!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }
        #endregion

        #region Methods
        public virtual void SelectButton() { }

        public virtual void HoverButton(bool hover)
        {
            _Animator.Play(hover ? "Hover" : "Idle");
        }
        #endregion
    }
}
