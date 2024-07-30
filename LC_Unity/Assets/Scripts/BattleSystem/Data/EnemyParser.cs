using System.Xml;
using UnityEngine;
using BattleSystem.Model;
using System;
using Logging;
using Utils;
using System.Globalization;
using System.Collections.Generic;
using Abilities;
using Actors;

namespace BattleSystem.Data
{
    public class EnemyParser
    {
        public Battler ParseEnemy(TextAsset file, int id)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(file.text);

                XmlNodeList enemies = doc.SelectSingleNode("Enemies").SelectNodes("Enemy");
                bool found = false;
                int matchingId = -1;
                int i = 0;

                for(i = 0; i < enemies.Count && !found; i++)
                {
                    matchingId = int.Parse(enemies[i].Attributes["Id"].InnerText);
                    found = matchingId == id;
                }

                if(found)
                {
                    Battler b = new Battler(matchingId,
                                            ParseStringValue(enemies[i - 1], "Name"),
                                            ParseIntValue(enemies[i - 1], "Level"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseHealth"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseMana"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseEssence"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseStrength"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseDefense"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseMagic"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseMagicDefense"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseAgility"),
                                            ParseStatScalingFunction(enemies[i - 1], "BaseLuck"),
                                            new List<Ability>(),
                                            new List<ActiveEffect>(),
                                            new List<ElementalAffinity>());

                    return b;
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Cannot parse enemy with ID " + id + ". Details = " + e.Message);
            }

            return null;
        }

        private static StatScalingFunction ParseStatScalingFunction(XmlNode parentNode, string tag)
        {
            XmlNode node = parentNode.SelectSingleNode(tag);
            StatScalingFunction func = new StatScalingFunction(ParseFloatAttribute(node, "A"),
                                                               ParseFloatAttribute(node, "Exponent"),
                                                               ParseFloatAttribute(node, "B"));

            return func;
        }

        private static float ParseFloatAttribute(XmlNode node, string attribute)
        {
            return float.Parse(node.Attributes[attribute].InnerText, CultureInfo.InvariantCulture);
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
