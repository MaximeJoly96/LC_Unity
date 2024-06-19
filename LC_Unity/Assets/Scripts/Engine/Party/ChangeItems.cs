using Engine.Events;
using UnityEngine.Events;
using Party;

namespace Engine.Party
{
    public class ChangeItems : IRunnable
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeItems()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            PartyManager.Instance.ChangeItems(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
