using UnityEngine;
using Inputs;

namespace Dialogs
{
    public abstract class CanvasMessageController : MonoBehaviour
    {
        protected InputController _inputController;

        [SerializeField]
        protected Canvas _canvas;

        protected virtual void Start()
        {
            _inputController = FindObjectOfType<InputController>();
        }
    }
}