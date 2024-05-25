using Engine.Events;

namespace Engine.Actor
{
    public class ChangeName : IRunnable
    {
        public int CharacterId { get; set; }
        public string Value { get; set; }

        public void Run()
        {

        }
    }
}
