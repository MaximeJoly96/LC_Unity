using Engine.Events;

namespace Engine.SceneControl
{
    public class BattleProcessing : IRunnable
    {
        public int TroopId { get; set; }
        public bool FromRandomEncounter { get; set; }
        public bool CanEscape { get; set; }
        public bool DefeatAllowed { get; set; }

        public void Run()
        {

        }
    }
}
