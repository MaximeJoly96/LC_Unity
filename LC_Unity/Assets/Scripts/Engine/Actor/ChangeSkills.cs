using Engine.Events;
using UnityEngine.Events;

namespace Engine.Actor
{
    public class ChangeSkills : IRunnable
    {
        public enum ActionType { Learn, Forget }

        public int TargetCount { get; set; }
        public int SkillId { get; set; }
        public ActionType Action { get; set; }
        public UnityEvent Finished { get; set; }

        public ChangeSkills()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
