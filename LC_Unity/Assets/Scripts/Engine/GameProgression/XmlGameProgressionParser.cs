using System.Xml;
using System;

namespace Engine.GameProgression
{
    public static class XmlGameProgressionParser
    {
        public static ControlSwitch ParseControlSwitch(XmlNode data)
        {
            ControlSwitch ctrlSwitch = new ControlSwitch();

            ctrlSwitch.Value = bool.Parse(data.Attributes["Value"].InnerText);

            return ctrlSwitch;
        }

        public static ControlVariable ParseControlVariable(XmlNode data)
        {
            ControlVariable ctrlVariable = new ControlVariable();

            ctrlVariable.Operation = (ControlVariable.Operator)Enum.Parse(typeof(ControlVariable.Operator), data.SelectSingleNode("Operation").InnerText);
            XmlNode operandNode = data.SelectSingleNode("Operand");

            ControlVariable.OperandType operand = (ControlVariable.OperandType)Enum.Parse(typeof(ControlVariable.OperandType), operandNode.Attributes["Type"].InnerText);
            ctrlVariable.Operand = operand;

            switch(operand)
            {
                case ControlVariable.OperandType.Constant:
                    ctrlVariable.AddValue(int.Parse(operandNode.InnerText));
                    break;
                case ControlVariable.OperandType.Random:
                    string innerText = operandNode.InnerText;
                    string[] values = innerText.Split(',');
                    foreach (string v in values)
                        ctrlVariable.AddValue(int.Parse(v));
                    break;
                case ControlVariable.OperandType.Variable:
                    break;
            }

            return ctrlVariable;
        }

        public static ControlTimer ParseControlTimer(XmlNode data)
        {
            ControlTimer ctrlTimer = new ControlTimer();

            ctrlTimer.Action = (ControlTimer.TimerAction)Enum.Parse(typeof(ControlTimer.TimerAction), data.SelectSingleNode("Action").InnerText);

            if (ctrlTimer.Action == ControlTimer.TimerAction.Start)
                ctrlTimer.Duration = int.Parse(data.SelectSingleNode("Duration").InnerText);

            return ctrlTimer;
        }
    }
}
