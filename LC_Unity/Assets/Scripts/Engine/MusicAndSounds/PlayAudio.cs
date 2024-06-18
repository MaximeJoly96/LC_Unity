using Engine.Events;
using UnityEngine.Events;

namespace Engine.MusicAndSounds
{
    public abstract class PlayAudio : IRunnable
    {
        public string Name { get; set; }
        public float Volume { get; set; }
        public float Pitch { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        protected PlayAudio()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
