using UnityEngine;
using Engine.Events;
using UnityEngine.Events;

namespace Engine.ScreenEffects
{
    public class FlashScreen : IRunnable
    {
        public Color TargetColor { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public FlashScreen()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
