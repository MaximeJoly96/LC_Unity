using System.Collections.Generic;
using System.Linq;
using Core.Model;
using System.Text;
using System;

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

        public Quest(Quest quest) : this(quest.Id, quest.NameKey, quest.DescriptionKey, quest.Type, quest.Reward)
        {
            foreach (QuestStep step in quest.Steps)
                AddStep(step);

            Status = quest.Status;
        }

        public Quest(int id, QuestStatus status)
        {
            _identifier = new ElementIdentifier(id, "", "");
            Status = status;

            Steps = new List<QuestStep>();
            Reward = new QuestReward();
            Type = QuestType.Main;
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

        public QuestStep GetStep(int id)
        {
            return Steps.FirstOrDefault(s => s.Id == id);
        }

        public string Serialize()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(SerializeId());
            builder.Append(";");
            builder.Append(Status.ToString());

            return builder.ToString();
        }

        public string SerializeId()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("quest");
            builder.Append(Id.ToString());

            return builder.ToString();
        }

        public static Quest Deserialize(string serializedVersion)
        {
            string[] split = serializedVersion.Split(';');

            int id = int.Parse(split[0].Replace("quest", ""));
            QuestStatus status = (QuestStatus)Enum.Parse(typeof(QuestStatus), split[1]);

            return new Quest(id, status);
        }
    }
}
