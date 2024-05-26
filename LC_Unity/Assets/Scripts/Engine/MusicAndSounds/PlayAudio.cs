using Engine.Events;

namespace Engine.MusicAndSounds
{
    public abstract class PlayAudio : IRunnable
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Pitch { get; set; }

        public abstract void Run();
    }
}
