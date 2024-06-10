using Engine.Events;
using UnityEngine.Events;

namespace Engine.MusicAndSounds
{
    public abstract class FadeOutAudio : IRunnable
    {
        public string Name { get; set; }
        public int TransitionDuration { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        protected FadeOutAudio()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
