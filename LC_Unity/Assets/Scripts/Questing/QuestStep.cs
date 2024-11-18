using Core.Model;
using System;
using System.Text;

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

        public QuestStep(int id, QuestStepStatus status) : this(id, "", "", new QuestReward())
        {
            Status = status;
        }

        public void ChangeStatus(QuestStepStatus status)
        {
            Status = status;
        }

        public string Serialize()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("step");
            builder.Append(Id.ToString());
            builder.Append(";");
            builder.Append(Status.ToString());

            return builder.ToString();
        }

        public static QuestStep Deserialize(string serializedVersion)
        {
            string[] split = serializedVersion.Split(';');
            int id = int.Parse(split[0].Replace("step", ""));
            QuestStepStatus status = (QuestStepStatus)Enum.Parse(typeof(QuestStepStatus), split[1]);

            return new QuestStep(id, status);
        }

        public void Update(QuestStep step)
        {
            _identifier = new ElementIdentifier(step.Id, step.NameKey, step.DescriptionKey);
            Reward = step.Reward;
        }
    }
}
