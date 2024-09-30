using System;
using System.Xml;
using UnityEngine;
using Logging;
using System.Collections.Generic;
using System.Globalization;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class BehaviourParser
    {
        public AiScript ParseBehaviour(TextAsset file)
        {
            AiScript script = new AiScript();

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode behaviourNode = document.SelectSingleNode("Behaviour");
                XmlNode mainConditionNode = behaviourNode.ChildNodes[0];
                script.SetMainCondition(ParseMainCondition(mainConditionNode));
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Cannot parse enemy behaviour. Reason: " + e.Message);
            }

            return script;
        }

        public BehaviourCondition ParseMainCondition(XmlNode node)
        {
            string type = node.Name;
            BehaviourCondition condition;

            if (type == typeof(IsInRange).Name)
            {
                condition = ParseIsInRange(node);
            }
            else if (type == typeof(HasEnoughResources).Name)
            {
                condition = ParseHasEnoughResources(node);
            }
            else if (type == typeof(DefaultCondition).Name)
            {
                condition = new DefaultCondition();
            }
            else
                throw new InvalidOperationException("Provided BehaviourCondition does not exist : " + type);

            condition.SetActions(ParseBehaviourAction(true, node), ParseBehaviourAction(false, node));

            return condition;
        }

        private IsInRange ParseIsInRange(XmlNode node)
        {
            return new IsInRange(ParseIntAttribute(node, "MinTargetCount"),
                                 ParseIntAttribute(node, "MaxTargetCount"),
                                 ParseIntAttribute(node, "Range"));

        }

        private HasEnoughResources ParseHasEnoughResources(XmlNode node)
        {
            Effects.Stat resource = (Effects.Stat)Enum.Parse(typeof(Effects.Stat), node.Attributes["Resource"].InnerText);
            HasEnoughResources.AmountType amount = (HasEnoughResources.AmountType)Enum.Parse(typeof(HasEnoughResources.AmountType), 
                                                                                             node.Attributes["Amount"].InnerText);
            float value = 0.0f;

            switch(amount)
            {
                case HasEnoughResources.AmountType.Flat:
                    value = ParseFloatAttribute(node, "Value");
                    break;
                case HasEnoughResources.AmountType.FromAbility:
                    value = ParseFloatAttribute(node, "Id");
                    break;
                case HasEnoughResources.AmountType.Percentage:
                    value = ParseFloatAttribute(node, "Threshold");
                    break;
            }

            return new HasEnoughResources(amount, value, resource);
        }

        private BehaviourAction ParseBehaviourAction(bool conditionValue, XmlNode parentNode)
        {
            BehaviourAction action = new BehaviourAction();

            XmlNode conditionNode = parentNode.SelectSingleNode(conditionValue.ToString().ToLower());
            if (conditionNode != null)
            {
                XmlNodeList abilityNodes = conditionNode.SelectNodes("Ability");
                if(abilityNodes.Count > 0)
                {
                    for (int i = 0; i < abilityNodes.Count; i++)
                        action.AddAbility(ParseIntAttribute(abilityNodes[i], "Id"));
                }
                else
                {
                    action.SetCondition(ParseMainCondition(conditionNode.ChildNodes[0]));
                }
            }

            return action;
        }

        private static int ParseIntAttribute(XmlNode node, string attributeKey)
        {
            XmlAttribute rangeAttribute = node.Attributes[attributeKey];
            return rangeAttribute != null ? int.Parse(rangeAttribute.InnerText) : 0;
        }

        private static float ParseFloatAttribute(XmlNode node, string attributeKey)
        {
            XmlAttribute rangeAttribute = node.Attributes[attributeKey];
            return rangeAttribute != null ? float.Parse(rangeAttribute.InnerText, CultureInfo.InvariantCulture) : 0.0f;
        }
    }
}
