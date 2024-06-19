using System.Xml;
using System;
using Logging;
using UnityEngine;

namespace Actors
{
    public static class XmlCharacterParser
    {
        public static Character ParseCharacter(TextAsset file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode characterNode = document.SelectSingleNode("Character");

                return new Character(ParseIntValue(characterNode, "Id"),
                                     ParseStringValue(characterNode, "Name"));
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlCharacterParser cannot parse Character. Exception: " + e.Message);
                return new Character();
            }
        }

        private static string ParseStringValue(XmlNode parentNode, string tag)
        {
            return parentNode.SelectSingleNode(tag).InnerText;
        }

        private static int ParseIntValue(XmlNode parentNode, string tag)
        {
            return int.Parse(ParseStringValue(parentNode, tag));
        }
    }
}
