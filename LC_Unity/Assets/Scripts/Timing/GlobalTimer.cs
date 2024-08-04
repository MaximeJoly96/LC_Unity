using UnityEngine;

namespace Timing
{
    public class GlobalTimer : MonoBehaviour
    {
        public bool Running { get; set; }
        public float InGameTimeSeconds { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Running)
                InGameTimeSeconds += Time.deltaTime;
        }

        public void InitInGameTimer(float timer)
        {
            InGameTimeSeconds = timer;
        }
    }
}
