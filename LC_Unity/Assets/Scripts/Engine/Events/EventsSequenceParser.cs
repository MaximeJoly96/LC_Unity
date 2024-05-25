using System.Xml;
using UnityEngine;
using System;
using Engine.Message;
using Engine.GameProgression;
using Engine.FlowControl;

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
                }
            }

            return sequence;
        }


    }
}
