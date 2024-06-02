using UnityEngine;
using Inputs;

namespace Dialogs
{
    public abstract class CanvasMessageController : MonoBehaviour
    {
        protected const float SELECTION_DELAY = 0.2f; // seconds

        protected InputController _inputController;
        protected float _selectionDelayCount;
        protected bool _delayOn;

        [SerializeField]
        protected Canvas _canvas;

        protected virtual void Start()
        {
            _inputController = FindObjectOfType<InputController>();
            _inputController.ButtonClicked.AddListener(ReceiveInput);
            _selectionDelayCount = 0.0f;
        }

        protected abstract void ReceiveInput(InputAction input);

        protected void Update()
        {
            if(_delayOn)
            {
                _selectionDelayCount += Time.deltaTime;
                if(_selectionDelayCount > SELECTION_DELAY)
                {
                    _selectionDelayCount = 0.0f;
                    _delayOn = false;
                }
            }
        }

        protected void StartSelectionDelay()
        {
            _delayOn = true;
        }
    }
}