using System.Xml;
using System;
using Logging;

namespace Engine.Message
{
    public static class XmlMessageParser
    {
        public static DisplayDialog ParseDialogData(XmlNode data)
        {
            DisplayDialog dialog = new DisplayDialog();

            try
            {
                dialog.BoxStyle = (DialogBoxStyle)Enum.Parse(typeof(DialogBoxStyle), data.SelectSingleNode("DialogBoxStyle").InnerText);
                dialog.BoxPosition = (DialogBoxPosition)Enum.Parse(typeof(DialogBoxPosition), data.SelectSingleNode("DialogBoxPosition").InnerText);
                dialog.Locutor = data.SelectSingleNode("Locutor").InnerText;
                dialog.Message = data.SelectSingleNode("Message").InnerText;
                dialog.FaceGraphics = data.SelectSingleNode("FaceGraphics").InnerText;
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlMessageParser cannot parse DisplayDialog. Exception: " + e.Message);
            }

            return dialog;
        }

        public static DisplayChoiceList ParseChoiceListData(XmlNode data)
        {
            DisplayChoiceList choiceList = new DisplayChoiceList();

            try
            {
                choiceList.Message = data.SelectSingleNode("Message").InnerText;
                XmlNode choices = data.SelectSingleNode("Choices");

                foreach (XmlNode child in choices.ChildNodes)
                {
                    Choice choice = new Choice();

                    choice.Id = child.Attributes["Id"].InnerText;
                    choice.Text = child.InnerText;

                    choiceList.Add(choice);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlMessageParser cannot parse DisplayChoiceList. Exception: " + e.Message);
            }

            return choiceList;
        }

        public static DisplayInputNumber ParseInputNumberData(XmlNode data)
        {
            DisplayInputNumber inputNumber = new DisplayInputNumber();

            try
            {
                inputNumber.DigitsCount = int.Parse(data.Attributes["DigitsCount"].InnerText);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlMessageParser cannot parse DisplayInputNumber. Exception: " + e.Message);
            }

            return inputNumber;
        }
    }
}
