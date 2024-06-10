using Engine.Events;
using UnityEngine.Events;

namespace Engine.Party
{
    public class ChangeGold : IRunnable
    {
        public int Value { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeGold()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
