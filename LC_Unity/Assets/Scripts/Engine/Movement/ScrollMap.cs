using Engine.Events;
using UnityEngine.Events;

namespace Engine.Movement
{
    public class ScrollMap : IRunnable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        public UnityEvent Finished { get; set; }

        public ScrollMap()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
