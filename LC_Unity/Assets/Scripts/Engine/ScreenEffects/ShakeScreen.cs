using Engine.Events;

namespace Engine.ScreenEffects
{
    public class ShakeScreen : IRunnable
    {
        public int Power { get; set; }
        public int Speed { get; set; }
        public int Duration { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
