using System.Xml;
using System;

namespace Engine.FlowControl
{
    public static class XmlFlowControlParser
    {
        public static ConditionalBranch ParseConditionalBranch(XmlNode data)
        {
            string type = data.Attributes["Type"].InnerText;
            string firstMember = data.Attributes["FirstMember"].InnerText;
            string secondMember = data.Attributes["SecondMember"].InnerText;

            if (type == typeof(SwitchCondition).Name)
            {
                SwitchCondition.Type conditionValue = (SwitchCondition.Type)Enum.Parse(typeof(SwitchCondition.Type), data.Attributes["Condition"].InnerText);
                SwitchCondition condition = new SwitchCondition();
                condition.Condition = conditionValue;
                condition.FirstMember = firstMember;
                condition.SecondMember = secondMember;

                return condition;
            }
            else if (type == typeof(VariableCondition).Name)
            {
                VariableCondition.Type conditionType = (VariableCondition.Type)Enum.Parse(typeof(VariableCondition.Type), data.Attributes["Condition"].InnerText);
                VariableCondition condition = new VariableCondition();
                condition.Condition = conditionType;
                condition.FirstMember = firstMember;
                condition.SecondMember = secondMember;

                return condition;
            }
            else if (type == typeof(TimerCondition).Name)
            {
                TimerCondition.Type conditionType = (TimerCondition.Type)Enum.Parse(typeof(TimerCondition.Type), data.Attributes["Condition"].InnerText);
                TimerCondition condition = new TimerCondition();
                condition.Condition = conditionType;
                condition.FirstMember = firstMember;
                condition.SecondMember = secondMember;

                return condition;
            }
            else
                throw new InvalidOperationException("Unsupported ConditionalBranch type");
        }
    }
}
