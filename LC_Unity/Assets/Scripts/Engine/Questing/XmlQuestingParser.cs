using System.Xml;
using System;
using Logging;
using Questing;

namespace Engine.Questing
{
    public class XmlQuestingParser
    {
        public static StartQuest ParseStartQuest(XmlNode node)
        {
            StartQuest start = new StartQuest();

            try
            {
                start.Id = int.Parse(node.Attributes["Id"].InnerText);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse StartQuest. Reason: " + e.Message);
            }

            return start;
        }

        public static FailQuest ParseFailQuest(XmlNode node)
        {
            FailQuest fail = new FailQuest();

            try
            {
                fail.Id = int.Parse(node.Attributes["Id"].InnerText);
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse FailQuest. Reason: " + e.Message);
            }

            return fail;
        }

        public static ProgressQuest ParseProgressQuest(XmlNode node)
        {
            ProgressQuest progress = new ProgressQuest();

            try
            {
                progress.Id = int.Parse(node.Attributes["Id"].InnerText);
                progress.StepId = int.Parse(node.Attributes["StepId"].InnerText);
                progress.StepStatus = (QuestStepStatus)Enum.Parse(typeof(QuestStepStatus), node.Attributes["Status"].InnerText);
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse ProgressQuest. Reason: " + e.Message);
            }

            return progress;
        }

        public static CompleteQuest ParseCompleteQuest(XmlNode node)
        {
            CompleteQuest complete = new CompleteQuest();

            try
            {
                complete.Id = int.Parse(node.Attributes["Id"].InnerText);
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogError("Could not parse CompleteQuest. Reason: " + e.Message);
            }

            return complete;
        }
    }
}
