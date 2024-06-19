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

                return new Character(ParseSpecificStat("Health", characterNode),
                                     ParseSpecificStat("Mana", characterNode),
                                     ParseSpecificStat("Essence", characterNode),
                                     ParseSpecificStat("Strength", characterNode),
                                     ParseSpecificStat("Defense", characterNode),
                                     ParseSpecificStat("Magic", characterNode),
                                     ParseSpecificStat("MagicDefense", characterNode),
                                     ParseSpecificStat("Agility", characterNode),
                                     ParseSpecificStat("Luck", characterNode));
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlCharacterParser cannot parse Character. Exception: " + e.Message);
                return new Character();
            }
        }

        private static int ParseSpecificStat(string stat, XmlNode parentNode)
        {
            return int.Parse(parentNode.SelectSingleNode(stat).InnerText);
        }
    }
}
