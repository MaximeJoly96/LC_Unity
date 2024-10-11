using UnityEngine;
using Core;

namespace Dialogs
{
    public abstract class CanvasMessageController : MonoBehaviour
    {
        protected bool _busy;
        protected InputReceiver _inputReceiver;

        [SerializeField]
        protected Canvas _canvas;

        protected virtual void Start()
        {
            _inputReceiver = GetComponent<InputReceiver>();
            BindInputs();
        }

        protected abstract void BindInputs();
        protected abstract bool CanReceiveInput();
    }
}