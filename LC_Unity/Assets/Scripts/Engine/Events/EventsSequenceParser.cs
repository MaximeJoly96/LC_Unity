using System.Xml;
using UnityEngine;
using System;
using Engine.Message;
using Engine.GameProgression;
using Engine.FlowControl;
using Engine.Party;
using Engine.Actor;

namespace Engine.Events
{
    public class EventsSequenceParser
    {
        public EventsSequence ParseEventsSequence(TextAsset file)
        {
            EventsSequence sequence = new EventsSequence();

            XmlDocument document = new XmlDocument();
            document.LoadXml(file.text);

            XmlNode eventsSequenceNode = document.SelectSingleNode("EventsSequence");

            foreach(XmlNode evt in eventsSequenceNode)
            {
                string nodeName = evt.Name;
                EventType eventType = (EventType)Enum.Parse(typeof(EventType), nodeName);

                switch(eventType)
                {
                    case EventType.DisplayDialog:
                        sequence.Add(XmlMessageParser.ParseDialogData(evt));
                        break;
                    case EventType.DisplayChoice:
                        sequence.Add(XmlMessageParser.ParseChoiceListData(evt));
                        break;
                    case EventType.InputNumber:
                        sequence.Add(XmlMessageParser.ParseInputNumberData(evt));
                        break;
                    case EventType.ControlSwitch:
                        sequence.Add(XmlGameProgressionParser.ParseControlSwitch(evt));
                        break;
                    case EventType.ControlVariable:
                        sequence.Add(XmlGameProgressionParser.ParseControlVariable(evt));
                        break;
                    case EventType.ControlTimer:
                        sequence.Add(XmlGameProgressionParser.ParseControlTimer(evt));
                        break;
                    case EventType.ConditionalBranch:
                        sequence.Add(XmlFlowControlParser.ParseConditionalBranch(evt));
                        break;
                    case EventType.ChangeGold:
                        sequence.Add(XmlPartyParser.ParseChangeGold(evt));
                        break;
                    case EventType.ChangeItems:
                        sequence.Add(XmlPartyParser.ParseChangeItems(evt));
                        break;
                    case EventType.ChangePartyMember:
                        sequence.Add(XmlPartyParser.ParseChangePartyMember(evt));
                        break;
                    case EventType.RecoverAll:
                        sequence.Add(XmlActorParser.ParseRecoverAll(evt));
                        break;
                    case EventType.ChangeExp:
                        sequence.Add(XmlActorParser.ParseChangeExp(evt));
                        break;
                    case EventType.ChangeLevel:
                        sequence.Add(XmlActorParser.ParseChangeLevel(evt));
                        break;
                    case EventType.ChangeSkills:
                        sequence.Add(XmlActorParser.ParseChangeSkills(evt));
                        break;
                    case EventType.ChangeEquipment:
                        sequence.Add(XmlActorParser.ParseChangeEquipment(evt));
                        break;
                    case EventType.ChangeName:
                        sequence.Add(XmlActorParser.ParseChangeName(evt));
                        break;
                }
            }

            return sequence;
        }


    }
}
