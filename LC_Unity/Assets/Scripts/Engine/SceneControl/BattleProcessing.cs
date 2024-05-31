using Engine.Events;
using UnityEngine.Events;

namespace Engine.SceneControl
{
    public class BattleProcessing : IRunnable
    {
        public int TroopId { get; set; }
        public bool FromRandomEncounter { get; set; }
        public bool CanEscape { get; set; }
        public bool DefeatAllowed { get; set; }
        public UnityEvent Finished { get; set; }

        public BattleProcessing()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
