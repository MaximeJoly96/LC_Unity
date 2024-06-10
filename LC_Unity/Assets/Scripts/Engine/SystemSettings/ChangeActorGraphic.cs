using Engine.Events;
using UnityEngine.Events;

namespace Engine.SystemSettings
{
    public class ChangeActorGraphic : IRunnable
    {
        public int CharacterId { get; set; }
        public string Charset { get; set; }
        public string Faceset { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeActorGraphic()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
