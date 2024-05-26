using Engine.Events;

namespace Engine.SystemSettings
{
    public class ChangeActorGraphic : IRunnable
    {
        public int CharacterId { get; set; }
        public string Charset { get; set; }
        public string Faceset { get; set; }

        public void Run()
        {

        }
    }
}
