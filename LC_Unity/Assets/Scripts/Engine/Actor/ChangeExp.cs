using Engine.Events;

namespace Engine.Actor
{
    public class ChangeExp : IRunnable
    {
        public int TargetId { get; set; }
        public int Amount { get; set; }

        public void Run()
        {

        }
    }
}
