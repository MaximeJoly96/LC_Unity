using Engine.Events;
using UnityEngine.Events;

namespace Engine.SystemSettings
{
    public abstract class ChangeBattleAudio : IRunnable
    {
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Pitch { get; set; }
        public UnityEvent Finished { get; set; }

        protected ChangeBattleAudio()
        {
            Finished = new UnityEvent();
        }

        public abstract void Run();
    }
}
