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
            int range = int.Parse(node.SelectSingleNode("Range").InnerText);

            XmlNode costNode = node.SelectSingleNode("Cost");
            AbilityCost cost = new AbilityCost(int.Parse(costNode.Attributes["HP"].InnerText),
                                               int.Parse(costNode.Attributes["MP"].InnerText),
                                               int.Parse(costNode.Attributes["EP"].InnerText));

            TargetEligibility eligibility = (TargetEligibility)Enum.Parse(typeof(TargetEligibility), node.SelectSingleNode("TargetEligibility").InnerText);
            AbilityCategory category = (AbilityCategory)Enum.Parse(typeof(AbilityCategory), node.SelectSingleNode("Category").InnerText);

            AbilityAnimation animation = ParseAbilityAnimation(node.SelectSingleNode("Animation"));

            Ability ability = new Ability(id, name, description, cost, usability, priority, eligibility, category);

            return ability;
        }

        private static AbilityAnimation ParseAbilityAnimation(XmlNode animationNode)
        {
            return new AbilityAnimation(animationNode.SelectSingleNode("Channel").InnerText,
                                        animationNode.SelectSingleNode("Strike").InnerText,
                                        int.Parse(animationNode.SelectSingleNode("ChannelParticles").InnerText),
                                        int.Parse(animationNode.SelectSingleNode("ImpactParticles").InnerText),
                                        int.Parse(animationNode.SelectSingleNode("ProjectileId").InnerText));
        }
    }
}
