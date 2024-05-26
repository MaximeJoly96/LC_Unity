using Engine.Events;

namespace Engine.MusicAndSounds
{
    public abstract class FadeOutAudio : IRunnable
    {
        public string Name { get; set; }
        public int TransitionDuration { get; set; }

        public abstract void Run();
    }
}
