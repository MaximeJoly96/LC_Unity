using Engine.Events;
using UnityEngine.Events;
using Party;

namespace Engine.Party
{
    public class ChangeGold : IRunnable
    {
        public int Value { get; set; }
        public bool Notify { get; set; } = true;
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeGold()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            PartyManager.Instance.ChangeGold(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
