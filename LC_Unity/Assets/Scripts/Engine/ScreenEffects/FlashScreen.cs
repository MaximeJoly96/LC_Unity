using UnityEngine;
using Engine.Events;

namespace Engine.ScreenEffects
{
    public class FlashScreen : IRunnable
    {
        public Color TargetColor { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
