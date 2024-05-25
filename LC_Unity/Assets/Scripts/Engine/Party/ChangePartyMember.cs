using Engine.Events;

namespace Engine.Party
{
    public class ChangePartyMember : IRunnable
    {
        public enum ActionType { Add, Remove }

        public int Id { get; set; }
        public ActionType Action { get; set; }
        public bool Initialize { get; set; }
        
        public void Run()
        {

        }
    }
}
