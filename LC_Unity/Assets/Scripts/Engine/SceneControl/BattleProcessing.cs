using Engine.Events;
using UnityEngine.Events;
using BattleSystem;

namespace Engine.SceneControl
{
    public class BattleProcessing : IRunnable
    {
        public int TroopId { get; set; }
        public bool FromRandomEncounter { get; set; }
        public bool CanEscape { get; set; }
        public bool DefeatAllowed { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public BattleProcessing()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            BattleLoader loader = new BattleLoader();
            loader.LoadBattle(this);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
