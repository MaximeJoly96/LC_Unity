using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class NameInputProcessing : IRunnable
    {
        public int CharacterId { get; set; }
        public int MaxCharacters { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public NameInputProcessing()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
