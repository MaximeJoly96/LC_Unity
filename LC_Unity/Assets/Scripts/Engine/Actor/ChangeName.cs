using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class ChangeName : IRunnable
    {
        public int CharacterId { get; set; }
        public string Value { get; set; }
        public UnityEvent Finished { get; set; }

        public ChangeName()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
