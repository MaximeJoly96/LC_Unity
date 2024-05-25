using UnityEngine;
using System.Xml;
using System;

namespace Engine.Message
{
    public class XmlDialogParser
    {
        public DisplayDialog ParseDialogData(TextAsset file)
        {
            DisplayDialog dialog = new DisplayDialog();

            XmlDocument document = new XmlDocument();
            document.LoadXml(file.text);
            XmlNode nodelist = document.SelectSingleNode("DialogData");

            dialog.BoxStyle = (DialogBoxStyle)Enum.Parse(typeof(DialogBoxStyle), nodelist.SelectSingleNode("DialogBoxStyle").InnerText);
            dialog.BoxPosition = (DialogBoxPosition)Enum.Parse(typeof(DialogBoxPosition), nodelist.SelectSingleNode("DialogBoxPosition").InnerText);
            dialog.Locutor = nodelist.SelectSingleNode("Locutor").InnerText;
            dialog.Message = nodelist.SelectSingleNode("Message").InnerText;
            dialog.BackgroundColor = new Color(float.Parse(nodelist.SelectSingleNode("BackgroundColor").Attributes["r"].InnerText) / 255.0f,
                                               float.Parse(nodelist.SelectSingleNode("BackgroundColor").Attributes["g"].InnerText) / 255.0f,
                                               float.Parse(nodelist.SelectSingleNode("BackgroundColor").Attributes["b"].InnerText) / 255.0f,
                                               float.Parse(nodelist.SelectSingleNode("BackgroundColor").Attributes["a"].InnerText) / 255.0f);
            dialog.FaceGraphics = nodelist.SelectSingleNode("FaceGraphics").InnerText;

            return dialog;
        }
    }
}
