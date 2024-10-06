using System.Xml;
using System;
using Logging;
using UnityEngine;
using System.Collections.Generic;
using Utils;
using System.Globalization;
using Essence;
using System.Linq;
using Core.Model;

namespace Actors
{
    public static class XmlCharacterParser
    {
        public static List<Character> ParseCharacters(TextAsset file)
        {
            List<Character> characters = new List<Character>();

            try
            {
                
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNodeList charactersNodes = document.SelectSingleNode("Characters").SelectNodes("Character");
                EssencesWrapper essencesWrapper = GameObject.FindObjectOfType<EssencesWrapper>();

                for (int i = 0; i < charactersNodes.Count; i++)
                {
                    ElementIdentifier identifier = new ElementIdentifier(ParseIntValue(charactersNodes[i], "Id"),
                                                                         ParseStringValue(charactersNodes[i], "Name"),
                                                                         "");

                    Character c = new Character(identifier,
                                                ParseQuadraticFunction(charactersNodes[i], "Exp"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseHealth"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseMana"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseEssence"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseStrength"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseDefense"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseMagic"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseMagicDefense"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseAgility"),
                                                ParseStatScalingFunction(charactersNodes[i], "BaseLuck"));

                    c.EssenceAffinity = essencesWrapper.EssentialAffinities.FirstOrDefault(e => e.Id == ParseIntValue(charactersNodes[i], 
                                                                                                                      "EssentialAffinity"));

                    characters.Add(c);
                }
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlCharacterParser cannot parse Character. Exception: " + e.Message);
            }

            return characters;
        }

        private static string ParseStringValue(XmlNode parentNode, string tag)
        {
            return parentNode.SelectSingleNode(tag).InnerText;
        }

        private static int ParseIntValue(XmlNode parentNode, string tag)
        {
            return int.Parse(ParseStringValue(parentNode, tag));
        }

        private static QuadraticFunction ParseQuadraticFunction(XmlNode parentNode, string tag)
        {
            XmlNode node = parentNode.SelectSingleNode(tag);
            QuadraticFunction func = new QuadraticFunction(ParseFloatAttribute(node, "A"),
                                                           ParseFloatAttribute(node, "B"),
                                                           ParseFloatAttribute(node, "C"));

            return func;
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
    }
}
