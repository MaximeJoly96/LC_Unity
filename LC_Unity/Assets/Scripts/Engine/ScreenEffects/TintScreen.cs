using Engine.Events;
using UnityEngine;

namespace Engine.ScreenEffects
{
    public class TintScreen : IRunnable
    {
        public Color TargetColor { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
