using Engine.Events;
using UnityEngine.Events;
using Actors;

namespace Engine.Actor
{
    public class ChangeEquipment : IRunnable
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeEquipment()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            CharactersManager.Instance.ChangeEquipment(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
