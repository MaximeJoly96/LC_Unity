using System;
using System.Xml;
using UnityEngine;
using Logging;
using System.Collections.Generic;

namespace BattleSystem.Behaviours
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

                for(int i = 0; i < behaviourNode.ChildNodes.Count; i++)
                {
                    script.AddComponent(ParseBehaviourComponent(behaviourNode.ChildNodes[i]));
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogError("Cannot parse enemy behaviour. Reason: " + e.Message);
            }

            return script;
        }

        private AiScriptComponent ParseBehaviourComponent(XmlNode componentNode)
        {
            AiScriptComponent scriptComponent = new AiScriptComponent();

            scriptComponent.Frequency = (AiScriptComponent.BehaviourFrequency)Enum.Parse(typeof(AiScriptComponent.BehaviourFrequency), 
                                                                                         componentNode.Name);
            scriptComponent.Priority = int.Parse(componentNode.Attributes["Priority"].InnerText);

            XmlNodeList abilitiesNodes = componentNode.SelectNodes("Ability");

            for(int i = 0; i < abilitiesNodes.Count; i++)
            {
                scriptComponent.AddAbility(int.Parse(abilitiesNodes[i].Attributes["Id"].InnerText));
            }

            return scriptComponent;
        }
    }
}
