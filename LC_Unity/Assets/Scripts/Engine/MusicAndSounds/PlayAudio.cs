using Engine.Events;
using UnityEngine.Events;

namespace Engine.MusicAndSounds
{
    public abstract class PlayAudio : IRunnable
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Pitch { get; set; }
        public UnityEvent Finished { get; set; }

        protected PlayAudio()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
