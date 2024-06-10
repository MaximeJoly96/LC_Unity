using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Engine.GameProgression;

namespace GameProgression
{
    public class TimersManager : MonoBehaviour
    {
        private List<Timer> _timers;

        protected List<Timer> Timers
        {
            get
            {
                if (_timers == null)
                    _timers = new List<Timer>();

                return _timers;
            }
        }

        public void AddTimer(ControlTimer ctrlTimer)
        {
            Timer timer = new Timer(ctrlTimer.Key, ctrlTimer.Duration);
            Timers.Add(timer);
        }

        protected virtual void Update()
        {
            for(int i = 0; i < Timers.Count; i++)
            {
                Timer currentTimer = Timers[i];

                if (currentTimer.CurrentState == Timer.State.Running)
                {
                    currentTimer.CurrentTime += Time.deltaTime;

                    if (currentTimer.CurrentTime >= currentTimer.MaxDuration)
                        currentTimer.Stop();
                }
            }
        }

        public void StartTimer(string key)
        {
            Timer timer = GetTimer(key);

            if (timer != null)
                timer.Run();
        }

        public void StopTimer(string key)
        {
            Timer timer = GetTimer(key);

            if (timer != null)
                timer.Stop();
        }

        public bool HasTimer(string key)
        {
            return GetTimer(key) != null;
        }

        public Timer GetTimer(string key)
        {
            return Timers.FirstOrDefault(t => t.Key == key);
        }
    }
}
