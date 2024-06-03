using System.Xml;
using System;

namespace Engine.GameProgression
{
    public static class XmlGameProgressionParser
    {
        public static ControlSwitch ParseControlSwitch(XmlNode data)
        {
            ControlSwitch ctrlSwitch = new ControlSwitch();

            ctrlSwitch.Key = data.Attributes["Key"].InnerText;

            if (data.Attributes["Source"] == null)
                ctrlSwitch.Value = bool.Parse(data.Attributes["Value"].InnerText);
            else
                ctrlSwitch.Source = data.Attributes["Source"].InnerText;

            return ctrlSwitch;
        }

        public static ControlVariable ParseControlVariable(XmlNode data)
        {
            ControlVariable ctrlVariable = new ControlVariable();

            ctrlVariable.Key = data.Attributes["Key"].InnerText;
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
                    ctrlVariable.Source = operandNode.InnerText;
                    break;
            }

            return ctrlVariable;
        }

        public static ControlTimer ParseControlTimer(XmlNode data)
        {
            ControlTimer ctrlTimer = new ControlTimer();

            ctrlTimer.Key = data.Attributes["Key"].InnerText;
            ctrlTimer.Action = (ControlTimer.TimerAction)Enum.Parse(typeof(ControlTimer.TimerAction), data.SelectSingleNode("Action").InnerText);

            if (ctrlTimer.Action == ControlTimer.TimerAction.Start)
            {
                if (data.Attributes["Source"] == null)
                    ctrlTimer.Duration = int.Parse(data.SelectSingleNode("Duration").InnerText);
                else
                    ctrlTimer.Source = data.Attributes["Source"].InnerText;
            }
                

            return ctrlTimer;
        }
    }
}
