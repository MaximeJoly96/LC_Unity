using Engine.Events;

namespace Engine.Character
{
    public class ShowAnimation : IRunnable
    {
        public string Target { get; set; }
        public int AnimationId { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
