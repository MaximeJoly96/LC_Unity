using System.Xml;
using System;
using Engine.Events;
using Logging;

namespace Engine.FlowControl
{
    public static class XmlFlowControlParser
    {
        public static ConditionalBranch ParseConditionalBranch(XmlNode data)
        {
            try
            {
                string type = data.Attributes["Type"].InnerText;
                string firstMember = data.Attributes["FirstMember"].InnerText;
                string secondMember = data.Attributes["SecondMember"].InnerText;

                if (type == typeof(SwitchCondition).Name)
                {
                    SwitchCondition.Type conditionValue = (SwitchCondition.Type)Enum.Parse(typeof(SwitchCondition.Type), data.Attributes["Condition"].InnerText);
                    SwitchCondition condition = new SwitchCondition
                    {
                        Condition = conditionValue,
                        FirstMember = firstMember,
                        SecondMember = secondMember,

                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else if (type == typeof(VariableCondition).Name)
                {
                    VariableCondition.Type conditionType = (VariableCondition.Type)Enum.Parse(typeof(VariableCondition.Type), data.Attributes["Condition"].InnerText);
                    VariableCondition condition = new VariableCondition
                    {
                        Condition = conditionType,
                        FirstMember = firstMember,
                        SecondMember = secondMember,

                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else if (type == typeof(TimerCondition).Name)
                {
                    TimerCondition.Type conditionType = (TimerCondition.Type)Enum.Parse(typeof(TimerCondition.Type), data.Attributes["Condition"].InnerText);
                    TimerCondition condition = new TimerCondition
                    {
                        Condition = conditionType,
                        FirstMember = firstMember,
                        SecondMember = secondMember,

                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else
                    throw new InvalidOperationException("Unsupported ConditionalBranch type. Found: " + type);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlFlowControlParser cannot parse ConditionalBranch. Exception: " + e.Message);
                return null;
            }
        }

        public static InventoryCondition ParseInventoryCondition(XmlNode data)
        {
            try
            {
                string type = data.Attributes["Type"].InnerText;
                int itemId = int.Parse(data.Attributes["ItemId"].InnerText);

                if (type == typeof(ItemPossessed).Name)
                {
                    ItemPossessed itemPossessed = new ItemPossessed
                    {
                        ItemId = itemId,
                        MinQuantity = int.Parse(data.Attributes["MinQuantity"].InnerText),

                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };
                    return itemPossessed;
                }
                else if (type == typeof(ItemEquipped).Name)
                {
                    ItemEquipped itemEquipped = new ItemEquipped 
                    { 
                        ItemId = itemId,

                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };
                    return itemEquipped;
                }
                else
                    throw new InvalidOperationException("Unsupported Inventory condition type. Found: " + type);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlFlowControlParser cannot parse InventoryCondition. Exception: " + e.Message);
                return null;
            }
        }

        public static QuestCondition ParseQuestCondition(XmlNode data)
        {
            try
            {
                string type = data.Attributes["Type"].InnerText;
                int questId = int.Parse(data.Attributes["QuestId"].InnerText);

                if (type == "Completed")
                {
                    QuestCompletedCondition condition = new QuestCompletedCondition
                    {
                        QuestId = questId,
                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else if (type == "StepCompleted")
                {
                    QuestStepCompletedCondition condition = new QuestStepCompletedCondition
                    {
                        QuestId = questId,
                        QuestStepId = int.Parse(data.Attributes["StepId"].InnerText),
                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else if (type == "Failed")
                {
                    QuestFailedCondition condition = new QuestFailedCondition
                    {
                        QuestId = questId,
                        SequenceWhenTrue = ParseConditionResults(true, data),
                        SequenceWhenFalse = ParseConditionResults(false, data)
                    };

                    return condition;
                }
                else
                    throw new InvalidOperationException("Unsupported Quest condition type. Found: " + type);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlFlowControlParser cannot parse QuestCondition. Exception: " + e.Message);
                return null;
            }
        }

        private static EventsSequence ParseConditionResults(bool conditionSuccess, XmlNode node)
        {
            var child = node.SelectSingleNode(conditionSuccess.ToString().ToLower());

            if(child != null)
            {
                return EventsSequenceParser.ParseEventsSequence(child);
            }

            return new EventsSequence();
        }
    }
}
