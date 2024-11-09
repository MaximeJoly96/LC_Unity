using Core.Model;

namespace Questing
{
    public enum QuestStepStatus
    {
        Unlocked,
        Locked,
        Failed,
        Completed
    }

    public class QuestStep
    {
        private ElementIdentifier _identifier;

        public int Id { get { return _identifier.Id; } }
        public string NameKey { get { return _identifier.NameKey; } }
        public string DescriptionKey { get { return _identifier.DescriptionKey; } }
        public QuestStepStatus Status { get; private set; }
        public QuestReward Reward { get; private set; }

        public QuestStep(int id, string nameKey, string descriptionKey, QuestReward reward)
        {
            _identifier = new ElementIdentifier(id, nameKey, descriptionKey);
            Reward = reward;

            Status = QuestStepStatus.Locked;
        }

        public void ChangeStatus(QuestStepStatus status)
        {
            Status = status;
        }
    }
}
