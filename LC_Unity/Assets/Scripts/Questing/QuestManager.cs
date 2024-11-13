using System.Collections.Generic;
using System.Linq;
using Engine.Questing;
using Logging;
using UnityEngine;
using System.Text;

namespace Questing
{
    public class QuestManager
    {
        private List<Quest> _allQuests;

        private static QuestManager _instance;

        public static QuestManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new QuestManager();

                return _instance;
            }
        }

        public List<Quest> CompletedQuests
        {
            get { return _allQuests.Where(q => q.Status == QuestStatus.Completed).ToList(); }
        }

        public List<Quest> FailedQuests
        {
            get { return _allQuests.Where(q => q.Status == QuestStatus.Failed).ToList(); }
        }

        public List<Quest> RunningQuests
        {
            get { return _allQuests.Where(q => q.Status == QuestStatus.Running).ToList(); }
        }

        private QuestManager()
        {
            _allQuests = new List<Quest>();
        }

        public void StartQuest(StartQuest start)
        {
            // If the quest is already running, completed or failed, we cannot start it.
            if (_allQuests.Any(q => q.Id == start.Id))
            {
                LogsHandler.Instance.LogWarning("You are trying to start quest with ID " + start.Id + " but the quest is already running, " +
                                                "has already been completed or failed.");
                return;
            }

            AddQuestWithStatus(start.Id, QuestStatus.Running);
        }

        public void FailQuest(FailQuest fail)
        {
            Quest existingQuest = _allQuests.FirstOrDefault(q => q.Id == fail.Id);

            // If the quest has not even started, we cannot make it fail.
            if(existingQuest == null || existingQuest.Status == QuestStatus.NotRunning)
            {
                LogsHandler.Instance.LogWarning("You are trying to fail quest with ID " + fail.Id + " but the quest was not even started.");
                return;
            }

            // A quest cannot fail if it has already failed or been completed.
            if(existingQuest.Status == QuestStatus.Failed || existingQuest.Status == QuestStatus.Completed)
            {
                LogsHandler.Instance.LogWarning("You are trying to fail quest with ID " + fail.Id + " but the quest has already been completed or failed.");
                return;
            }

            existingQuest.ChangeStatus(QuestStatus.Failed);
        }

        public void CompleteQuest(CompleteQuest complete)
        {
            Quest existingQuest = _allQuests.FirstOrDefault(q => q.Id == complete.Id);

            // If the quest has not even started, we cannot complete it.
            if (existingQuest == null || existingQuest.Status == QuestStatus.NotRunning)
            {
                LogsHandler.Instance.LogWarning("You are trying to complete quest with ID " + complete.Id + " but the quest was not even started.");
                return;
            }

            // A quest cannot be completed if it has already failed or been completed.
            if (existingQuest.Status == QuestStatus.Failed || existingQuest.Status == QuestStatus.Completed)
            {
                LogsHandler.Instance.LogWarning("You are trying to complete quest with ID " + complete.Id + " but the quest has already been completed or failed.");
                return;
            }

            existingQuest.ChangeStatus(QuestStatus.Completed);
        }

        public void ProgressQuest(ProgressQuest progress)
        {
            Quest existingQuest = _allQuests.FirstOrDefault(q => q.Id == progress.Id);

            // If the quest has not even started, we cannot progress it.
            if (existingQuest == null || existingQuest.Status == QuestStatus.NotRunning)
            {
                LogsHandler.Instance.LogWarning("You are trying to progress quest with ID " + progress.Id + " but the quest was not even started.");
                return;
            }

            // A quest cannot be progressed if it has already failed or been completed.
            if (existingQuest.Status == QuestStatus.Failed || existingQuest.Status == QuestStatus.Completed)
            {
                LogsHandler.Instance.LogWarning("You are trying to progress quest with ID " + progress.Id + " but the quest has already been completed or failed.");
                return;
            }

            ProgressQuestStep(progress, existingQuest);
        }

        private void ProgressQuestStep(ProgressQuest progress, Quest quest)
        {
            QuestStep step = quest.GetStep(progress.StepId);

            if(step == null)
            {
                LogsHandler.Instance.LogWarning("You cannot progress step " + progress.StepId + " of quest " + quest.Id + " because this step " +
                                                "does not exist.");
                return;
            }

            step.ChangeStatus(progress.StepStatus);
        }

        public void Reset()
        {
            _allQuests.Clear();
        }

        public Quest GetQuest(int id)
        {
            return _allQuests.FirstOrDefault(q => q.Id == id);
        }

        private void AddQuestWithStatus(int id, QuestStatus status)
        {
            Quest quest = Object.FindObjectOfType<QuestsWrapper>().GetQuest(id);

            // We copy the quest data and create a new one with the default status. If we just kept the previous reference,
            // we might have conflicts if another object manipulates the same object.
            Quest toAdd = new Quest(quest);
            toAdd.ChangeStatus(status);
            _allQuests.Add(toAdd);
        }

        public string Serialize()
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < _allQuests.Count; i++)
            {
                builder.AppendLine(_allQuests[i].Serialize());
                string questId = _allQuests[i].SerializeId();

                for(int j = 0; j < _allQuests[i].Steps.Count; j++)
                {
                    builder.AppendLine(questId + _allQuests[i].GetStep(j).Serialize());
                }
            }

            return builder.ToString();
        }

        public void Deserialize(string serializedVersion)
        {
            Reset();
#if UNITY_ANDROID
            string[] split = serializedVersion.Split("\n");
#else
            string[] split = serializedVersion.Split("\r\n");
#endif

            for (int i = 0; i < split.Length && split[i].Contains("quest"); i++)
            {
                if (split[i].Contains("step"))
                {
                    // First we need the quest ID
                    string prefix = split[i].Split(';')[0];
                    int index = prefix.IndexOf("step");
                    string prefixWithoutStep = prefix.Substring(0, index);
                    int questId = int.Parse(prefixWithoutStep.Replace("quest", ""));

                    Quest matchingQuest = _allQuests.FirstOrDefault(q => q.Id == questId);
                    if(matchingQuest != null)
                    {
                        QuestStep step = QuestStep.Deserialize(split[i].Replace(prefixWithoutStep, ""));
                        matchingQuest.AddStep(step);
                    }
                    else
                        LogsHandler.Instance.LogWarning(prefix + " could not find a matching quest and will be ignored.");
                }
                else
                {
                    Quest quest = Quest.Deserialize(split[i]);
                    _allQuests.Add(quest);
                }
            }
        }
    }
}
