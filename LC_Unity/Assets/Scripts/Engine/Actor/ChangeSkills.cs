using Engine.Events;
using UnityEngine.Events;
using Actors;

namespace Engine.Actor
{
    public class ChangeSkills : IRunnable
    {
        public enum ActionType { Learn, Forget }

        public int CharacterId { get; set; }
        public int SkillId { get; set; }
        public ActionType Action { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeSkills()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            CharactersManager.Instance.ChangeSkills(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
