using UnityEngine;
using Inputs;
using Engine.Events;

namespace Dialogs
{
    public abstract class CanvasMessageController<T> : MonoBehaviour where T : UiBox<IRunnable>
    {
        protected const float SELECTION_DELAY = 0.2f; // seconds

        protected T _currentItem;
        protected InputController _inputController;
        protected float _selectionDelayCount;
        protected bool _delayOn;

        [SerializeField]
        protected Canvas _canvas;
        [SerializeField]
        protected T _itemPrefab;

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

        public void CreateItem(IRunnable item)
        {
            _currentItem = Instantiate(_itemPrefab, _canvas.transform);

            _currentItem.Feed(item);
            _currentItem.Open();
            _currentItem.HasClosed.AddListener(DestroyCurrentItem);
        }

        protected virtual void DestroyCurrentItem()
        {
            Destroy(_currentItem.gameObject);
        }
    }
}