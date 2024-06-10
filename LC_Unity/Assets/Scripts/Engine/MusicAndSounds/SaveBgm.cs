using Engine.Events;
using UnityEngine.Events;

namespace Engine.MusicAndSounds
{
    public class SaveBgm : IRunnable
    {
        public string Name { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public SaveBgm()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
