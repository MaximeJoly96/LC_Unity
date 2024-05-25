using Engine.Events;

namespace Engine.Actor
{
    public class ChangeLevel : IRunnable
    {
        public int TargetCount { get; set; }
        public int Amount { get; set; }

        public void Run()
        {

        }
    }
}
