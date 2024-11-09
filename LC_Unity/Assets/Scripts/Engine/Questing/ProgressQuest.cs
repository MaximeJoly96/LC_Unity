using Questing;

namespace Engine.Questing
{
    public class ProgressQuest : QuestOperation
    {
        public int StepId { get; set; }
        public QuestStepStatus StepStatus { get; set; }

        public override void Run()
        {
            QuestManager.Instance.ProgressQuest(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
