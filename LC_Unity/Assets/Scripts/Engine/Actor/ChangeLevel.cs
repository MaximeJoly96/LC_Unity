using Engine.Events;
using UnityEngine.Events;
using Actors;

namespace Engine.Actor
{
    public class ChangeLevel : IRunnable
    {
        public int CharacterId { get; set; }
        public int Amount { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeLevel()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            CharactersManager.Instance.ChangeCharacterLevel(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
