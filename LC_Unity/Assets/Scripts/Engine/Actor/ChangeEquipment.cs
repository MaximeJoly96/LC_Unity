using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class ChangeEquipment : IRunnable
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public UnityEvent Finished { get; set; }

        public ChangeEquipment()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
