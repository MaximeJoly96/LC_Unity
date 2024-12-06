using Questing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.FlowControl
{
    public class QuestCompletedCondition : QuestCondition
    {
        public override void Run()
        {
            DefineSequences(QuestManager.Instance.CompletedQuests.Any(q => q.Id == QuestId));
        }
    }
}
