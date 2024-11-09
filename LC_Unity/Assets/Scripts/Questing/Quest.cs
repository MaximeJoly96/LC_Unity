using System.Collections.Generic;
using Core.Model;

namespace Questing
{
    public enum QuestType
    {
        Main,
        Side,
        Bounty,
        Profession
    }

    public enum QuestStatus
    {
        NotRunning,
        Running,
        Completed,
        Failed
    }

    public class Quest
    {
        private ElementIdentifier _identifier;

        public int Id { get { return _identifier.Id; } }
        public string NameKey { get { return _identifier.NameKey; } }
        public string DescriptionKey { get { return _identifier.DescriptionKey; } }
        public QuestType Type { get; private set; }
        public List<QuestStep> Steps { get; private set; }
        public QuestReward Reward { get; private set; }
        public QuestStatus Status { get; private set; }

        public Quest(int id, string nameKey, string descriptionKey, QuestType type, QuestReward reward)
        {
            _identifier = new ElementIdentifier(id, nameKey, descriptionKey);
            Type = type;

            Steps = new List<QuestStep>();
            Reward = reward;

            Status = QuestStatus.NotRunning;
        }

        public static Quest DefaultQuest()
        {
            return new Quest(-1, "default", "default", QuestType.Main, new QuestReward(0, 0, new List<Inventory.InventoryItem>()));
        }

        public void AddStep(QuestStep step)
        {
            Steps.Add(step);
        }

        public void ChangeStatus(QuestStatus status)
        {
            Status = status;
        }
    }
}
