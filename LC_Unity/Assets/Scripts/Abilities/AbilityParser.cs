using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System;

namespace Abilities
{
    public static class AbilityParser
    {
        public static List<Ability> ParseAllAbilities(TextAsset file)
        {
            List<Ability> abilities = new List<Ability>();

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode mainNode = document.SelectSingleNode("Abilities");
                XmlNodeList abilitiesNodes = mainNode.SelectNodes("Ability");

                for(int i = 0; i < abilitiesNodes.Count; i++)
                {
                    abilities.Add(ParseAbility(abilitiesNodes[i]));
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("Could not parse abilities. See " + e.Message);
            }

            return abilities;
        }

        private static Ability ParseAbility(XmlNode node)
        {
            int id = int.Parse(node.SelectSingleNode("Id").InnerText);
            string name = node.SelectSingleNode("Name").InnerText;
            string description = node.SelectSingleNode("Description").InnerText;
            AbilityUsability usability = (AbilityUsability)Enum.Parse(typeof(AbilityUsability), node.SelectSingleNode("Usability").InnerText);
            int priority = int.Parse(node.SelectSingleNode("Priority").InnerText);

            XmlNode costNode = node.SelectSingleNode("Cost");
            AbilityCost cost = new AbilityCost(int.Parse(costNode.Attributes["Health"].InnerText),
                                                int.Parse(costNode.Attributes["Mana"].InnerText),
                                                int.Parse(costNode.Attributes["Essence"].InnerText));

            return new Ability(id, name, description, cost, usability, priority);
        }
    }
}
