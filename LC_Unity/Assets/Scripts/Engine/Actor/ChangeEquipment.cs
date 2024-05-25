using Engine.Events;

namespace Engine.Actor
{
    public class ChangeEquipment : IRunnable
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }

        public void Run()
        {

        }
    }
}
