using System.Xml;
using System;

namespace Engine.FlowControl
{
    public static class XmlFlowControlParser
    {
        public static ConditionalBranch ParseConditionalBranch(XmlNode data)
        {
            string type = data.Attributes["Type"].InnerText;

            if (type == typeof(SwitchCondition).Name)
            {
                bool conditionValue = bool.Parse(data.Attributes["Condition"].InnerText);
                SwitchCondition condition = new SwitchCondition();
                condition.Condition = conditionValue;

                return condition;
            }
            else if (type == typeof(VariableCondition).Name)
            {
                VariableCondition.Type conditionType = (VariableCondition.Type)Enum.Parse(typeof(VariableCondition.Type), data.Attributes["Condition"].InnerText);
                VariableCondition condition = new VariableCondition();
                condition.Condition = conditionType;

                return condition;
            }
            else if (type == typeof(TimerCondition).Name)
            {
                TimerCondition.Type conditionType = (TimerCondition.Type)Enum.Parse(typeof(TimerCondition.Type), data.Attributes["Condition"].InnerText);
                TimerCondition condition = new TimerCondition();
                condition.Condition = conditionType;

                return condition;
            }
            else
                throw new InvalidOperationException("Unsupported ConditionalBranch type");
        }
    }
}
