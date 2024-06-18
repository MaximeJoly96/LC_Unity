using UnityEngine;
using Engine.Events;
using UnityEngine.Events;
using ScreenEffects;

namespace Engine.ScreenEffects
{
    public class FlashScreen : IRunnable
    {
        public Color TargetColor { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public FlashScreen()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<ScreenEffectsHandler>().FlashScreen(this);
        }
    }
}
