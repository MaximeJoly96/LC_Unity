using UnityEngine;
using System.Xml;
using System;

namespace Engine.Message
{
    public static class XmlMessageParser
    {
        public static DisplayDialog ParseDialogData(XmlNode data)
        {
            DisplayDialog dialog = new DisplayDialog();

            dialog.BoxStyle = (DialogBoxStyle)Enum.Parse(typeof(DialogBoxStyle), data.SelectSingleNode("DialogBoxStyle").InnerText);
            dialog.BoxPosition = (DialogBoxPosition)Enum.Parse(typeof(DialogBoxPosition), data.SelectSingleNode("DialogBoxPosition").InnerText);
            dialog.Locutor = data.SelectSingleNode("Locutor").InnerText;
            dialog.Message = data.SelectSingleNode("Message").InnerText;
            dialog.BackgroundColor = new Color(float.Parse(data.SelectSingleNode("BackgroundColor").Attributes["r"].InnerText) / 255.0f,
                                               float.Parse(data.SelectSingleNode("BackgroundColor").Attributes["g"].InnerText) / 255.0f,
                                               float.Parse(data.SelectSingleNode("BackgroundColor").Attributes["b"].InnerText) / 255.0f,
                                               float.Parse(data.SelectSingleNode("BackgroundColor").Attributes["a"].InnerText) / 255.0f);
            dialog.FaceGraphics = data.SelectSingleNode("FaceGraphics").InnerText;

            return dialog;
        }

        public static DisplayChoiceList ParseChoiceListData(XmlNode data)
        {
            DisplayChoiceList choiceList = new DisplayChoiceList();

            XmlNode choices = data.SelectSingleNode("Choices");

            foreach(XmlNode child in choices.ChildNodes)
            {
                Choice choice = new Choice();

                choice.Id = child.Attributes["Id"].InnerText;
                choice.Text = child.InnerText;

                choiceList.Add(choice);
            }

            return choiceList;
        }

        public static DisplayInputNumber ParseInputNumberData(XmlNode data)
        {
            DisplayInputNumber inputNumber = new DisplayInputNumber();

            inputNumber.DigitsCount = int.Parse(data.Attributes["DigitsCount"].InnerText);

            return inputNumber;
        }
    }
}
