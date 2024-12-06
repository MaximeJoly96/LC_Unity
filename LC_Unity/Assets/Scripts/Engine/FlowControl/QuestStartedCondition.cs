using Questing;
using System.Linq;

namespace Engine.FlowControl
{
    public class QuestStartedCondition : QuestCondition
    {
        public override void Run()
        {
            DefineSequences(QuestManager.Instance.RunningQuests.Any(q => q.Id == QuestId) ||
                            QuestManager.Instance.CompletedQuests.Any(q => q.Id == QuestId) ||
                            QuestManager.Instance.FailedQuests.Any(q => q.Id == QuestId));
        }
    }
}
