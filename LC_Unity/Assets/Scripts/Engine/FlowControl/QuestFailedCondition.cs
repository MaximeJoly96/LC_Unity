using Questing;
using System.Linq;

namespace Engine.FlowControl
{
    public class QuestFailedCondition : QuestCondition
    {
        public override void Run()
        {
            DefineSequences(QuestManager.Instance.FailedQuests.Any(q => q.Id == QuestId));
        }
    }
}
