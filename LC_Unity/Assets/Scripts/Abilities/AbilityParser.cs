using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using Logging;
using System;
using Core.Model;
using Effects;

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
            ElementIdentifier identifier = ParseIdentifier(node);
            AbilityUsability usability = (AbilityUsability)Enum.Parse(typeof(AbilityUsability), node.SelectSingleNode("Usability").InnerText);
            TargetEligibility eligibility = (TargetEligibility)Enum.Parse(typeof(TargetEligibility), node.SelectSingleNode("TargetEligibility").InnerText);
            AbilityCategory category = (AbilityCategory)Enum.Parse(typeof(AbilityCategory), node.SelectSingleNode("Category").InnerText);
            int priority = int.Parse(node.SelectSingleNode("Priority").InnerText);
            int range = int.Parse(node.SelectSingleNode("Range").InnerText);

            Ability ability = new Ability(identifier, priority, usability, eligibility, category, range);

            XmlNode costNode = node.SelectSingleNode("Cost");
            AbilityCost cost = new AbilityCost(int.Parse(costNode.Attributes["HP"].InnerText),
                                               int.Parse(costNode.Attributes["MP"].InnerText),
                                               int.Parse(costNode.Attributes["EP"].InnerText));
            ability.SetCost(cost);

            AbilityAnimation animation = ParseAbilityAnimation(node.SelectSingleNode("Animation"));
            ability.SetAnimation(animation);

            List<IEffect> effects = ParseEffects(node.SelectSingleNode("Effects"));
            ability.SetEffects(effects);

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

        private static ElementIdentifier ParseIdentifier(XmlNode node)
        {
            return new ElementIdentifier(int.Parse(node.SelectSingleNode("Id").InnerText),
                                         node.SelectSingleNode("Name").InnerText,
                                         node.SelectSingleNode("Description").InnerText);
        }

        private static List<IEffect> ParseEffects(XmlNode effectsNode)
        {
            return EffectsParser.ParseEffectsFromNode(effectsNode);
        }
    }
}
