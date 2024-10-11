using UnityEngine;
using Core;

namespace TitleScreen
{
    public abstract class TitleScreenPanel : MonoBehaviour
    {
        protected InputReceiver _inputReceiver;

        protected int _cursorPosition;
        protected bool _lockedChoice;

        protected virtual void Start()
        {
            UpdateCursor();

            _inputReceiver = GetComponent<InputReceiver>();
            BindInputs();
        }

        protected abstract void BindInputs();
        protected abstract bool CanReceiveInput();

        protected abstract void UpdateCursor();

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Unlock()
        {
            _lockedChoice = false;
        }
    }
}
