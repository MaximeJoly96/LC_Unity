using Questing;

namespace Engine.Questing
{
    public class CompleteQuest : QuestOperation
    {
        public override void Run()
        {
            QuestManager.Instance.CompleteQuest(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
