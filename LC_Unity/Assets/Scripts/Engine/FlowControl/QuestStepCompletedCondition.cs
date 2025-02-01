using Questing;
using System.Linq;

namespace Engine.FlowControl
{
    public class QuestStepCompletedCondition : QuestCondition
    {
        public int QuestStepId { get; set; }

        public override void Run()
        {
            Quest relatedQuest = QuestManager.Instance.AllQuests.FirstOrDefault(q => q.Id == QuestId);

            if (relatedQuest == null)
            {
                DefineSequences(false);
                return;
            }
            
            QuestStep relatedStep = relatedQuest.Steps.FirstOrDefault(s => s.Id == QuestStepId);

            if (relatedStep == null)
            {
                DefineSequences(false);
                return;
            }

            DefineSequences(relatedStep.Status == QuestStepStatus.Completed);
        }
    }
}
