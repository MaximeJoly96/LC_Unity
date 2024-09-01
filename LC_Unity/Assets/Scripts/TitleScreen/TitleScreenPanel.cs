using UnityEngine;
using Inputs;
using System.Collections.Generic;

namespace TitleScreen
{
    public abstract class TitleScreenPanel : MonoBehaviour
    {
        protected const float SELECTION_DELAY = 0.2f; // seconds

        protected int _cursorPosition;
        protected float _selectionDelay;
        protected bool _delayOn;
        protected bool _lockedChoice;

        protected virtual void Start()
        {
            _selectionDelay = 0.0f;
            _delayOn = false;
            UpdateCursor();

            InputController inputCtrl = FindObjectOfType<InputController>();
            inputCtrl.ButtonClicked.AddListener(ReceiveInput);
        }

        protected void Update()
        {
            if (_delayOn && !_lockedChoice)
            {
                _selectionDelay += Time.deltaTime;
                if (_selectionDelay > SELECTION_DELAY)
                {
                    _selectionDelay = 0.0f;
                    _delayOn = false;
                }
            }
        }

        protected abstract void ReceiveInput(InputAction input);

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
