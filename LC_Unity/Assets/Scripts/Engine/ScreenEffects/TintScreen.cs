using Engine.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Engine.ScreenEffects
{
    public class TintScreen : IRunnable
    {
        public Color TargetColor { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public TintScreen()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
