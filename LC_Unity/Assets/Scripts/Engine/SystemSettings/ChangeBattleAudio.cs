using Engine.Events;

namespace Engine.SystemSettings
{
    public abstract class ChangeBattleAudio : IRunnable
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Pitch { get; set; }

        public abstract void Run();
    }
}
