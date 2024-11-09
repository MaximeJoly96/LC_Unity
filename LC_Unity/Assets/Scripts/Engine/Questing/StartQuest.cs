using Questing;

namespace Engine.Questing
{
    public class StartQuest : QuestOperation
    {
        public override void Run()
        {
            QuestManager.Instance.StartQuest(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
