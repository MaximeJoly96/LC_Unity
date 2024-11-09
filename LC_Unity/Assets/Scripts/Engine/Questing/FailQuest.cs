using Questing;

namespace Engine.Questing
{
    public class FailQuest : QuestOperation
    {
        public override void Run()
        {
            QuestManager.Instance.FailQuest(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
