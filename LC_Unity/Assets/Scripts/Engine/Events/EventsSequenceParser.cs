using System.Xml;
using UnityEngine;
using System;
using Engine.Message;
using Engine.GameProgression;
using Engine.FlowControl;
using Engine.Party;
using Engine.Actor;
using Engine.Movement;
using Engine.Character;
using Engine.ScreenEffects;
using Engine.Timing;
using Engine.PictureAndWeather;

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
                    case EventType.SetMoveRoute:
                        sequence.Add(XmlMovementParser.ParseSetMoveRoute(evt));
                        break;
                    case EventType.GetOnOffVehicle:
                        sequence.Add(XmlMovementParser.ParseGetOnOffVehicle(evt));
                        break;
                    case EventType.ScrollMap:
                        sequence.Add(XmlMovementParser.ParseScrollMap(evt));
                        break;
                    case EventType.TransferObject:
                        sequence.Add(XmlMovementParser.ParseTransferObject(evt));
                        break;
                    case EventType.ShowAnimation:
                        sequence.Add(XmlCharacterParser.ParseShowAnimation(evt));
                        break;
                    case EventType.ShowBalloonIcon:
                        sequence.Add(XmlCharacterParser.ParseShowBalloonIcon(evt));
                        break;
                    case EventType.ShakeScreen:
                        sequence.Add(XmlScreenEffectsParser.ParseShakeScreen(evt));
                        break;
                    case EventType.TintScreen:
                        sequence.Add(XmlScreenEffectsParser.ParseTintScreen(evt));
                        break;
                    case EventType.FlashScreen:
                        sequence.Add(XmlScreenEffectsParser.ParseFlashScreen(evt));
                        break;
                    case EventType.FadeScreen:
                        sequence.Add(XmlScreenEffectsParser.ParseFadeScreen(evt));
                        break;
                    case EventType.Wait:
                        sequence.Add(XmlTimingParser.ParseWait(evt));
                        break;
                    case EventType.MovePicture:
                        sequence.Add(XmlPictureAndWeatherParser.ParseMovePicture(evt));
                        break;
                    case EventType.RotatePicture:
                        sequence.Add(XmlPictureAndWeatherParser.ParseRotatePicture(evt));
                        break;
                    case EventType.SetWeatherEffects:
                        sequence.Add(XmlPictureAndWeatherParser.ParseSetWeatherEffects(evt));
                        break;
                    case EventType.ShowPicture:
                        sequence.Add(XmlPictureAndWeatherParser.ParseShowPicture(evt));
                        break;
                    case EventType.TintPicture:
                        sequence.Add(XmlPictureAndWeatherParser.ParseTintPicture(evt));
                        break;
                }
            }

            return sequence;
        }


    }
}
