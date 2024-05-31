using Engine.Events;
using UnityEngine.Events;

namespace Engine.Message
{
    public class DisplayInputNumber : IRunnable
    {
        public int DigitsCount { get; set; }
        public UnityEvent Finished { get; set; }

        public DisplayInputNumber()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
