using UnityEngine;
using UnityEngine.Events;

namespace Timing
{
    public class GlobalTimer : MonoBehaviour
    {
        private int _previousSeconds;
        private UnityEvent<int> _globalTimeChangedEvent;
        public UnityEvent<int> GlobalTimeChangedEvent
        {
            get
            {
                if (_globalTimeChangedEvent == null)
                    _globalTimeChangedEvent = new UnityEvent<int>();

                return _globalTimeChangedEvent;
            }
        }

        public bool Running { get; set; }
        public float InGameTimeSeconds { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Running)
            {
                InGameTimeSeconds += Time.deltaTime;

                if(_previousSeconds < Mathf.FloorToInt(InGameTimeSeconds))
                {
                    _previousSeconds++;
                    GlobalTimeChangedEvent.Invoke(_previousSeconds);
                }
            }  
        }

        public void InitInGameTimer(float timer)
        {
            InGameTimeSeconds = timer;
            _previousSeconds = Mathf.FloorToInt(InGameTimeSeconds);
        }
    }
}
