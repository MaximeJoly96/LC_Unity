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
using Engine.MusicAndSounds;
using Engine.SceneControl;
using Engine.SystemSettings;
using Engine.Map;
using Logging;

namespace Engine.Events
{
    public static class EventsSequenceParser
    {
        public static EventsSequence ParseEventsSequence(XmlNode baseNode)
        {
            EventsSequence sequence = new EventsSequence();

            try
            {
                foreach (XmlNode evt in baseNode)
                {
                    string nodeName = evt.Name;
                    EventType eventType = (EventType)Enum.Parse(typeof(EventType), nodeName);

                    switch (eventType)
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
                        case EventType.PlayBgm:
                            sequence.Add(XmlMusicAndSoundsParser.ParsePlayBgm(evt));
                            break;
                        case EventType.PlayBgs:
                            sequence.Add(XmlMusicAndSoundsParser.ParsePlayBgs(evt));
                            break;
                        case EventType.PlayMusicalEffect:
                            sequence.Add(XmlMusicAndSoundsParser.ParsePlayMusicalEffect(evt));
                            break;
                        case EventType.PlaySoundEffect:
                            sequence.Add(XmlMusicAndSoundsParser.ParsePlaySoundEffect(evt));
                            break;
                        case EventType.FadeOutBgm:
                            sequence.Add(XmlMusicAndSoundsParser.ParseFadeOutBgm(evt));
                            break;
                        case EventType.FadeOutBgs:
                            sequence.Add(XmlMusicAndSoundsParser.ParseFadeOutBgs(evt));
                            break;
                        case EventType.SaveBgm:
                            sequence.Add(XmlMusicAndSoundsParser.ParseSaveBgm(evt));
                            break;
                        case EventType.ReplayBgm:
                            sequence.Add(XmlMusicAndSoundsParser.ParseReplayBgm(evt));
                            break;
                        case EventType.BattleProcessing:
                            sequence.Add(XmlSceneControlParser.ParseBattleProcessing(evt));
                            break;
                        case EventType.ShopProcessing:
                            sequence.Add(XmlSceneControlParser.ParseShopProcessing(evt));
                            break;
                        case EventType.NameInputProcessing:
                            sequence.Add(XmlSceneControlParser.ParseNameInputProcessing(evt));
                            break;
                        case EventType.OpenMenu:
                            sequence.Add(XmlSceneControlParser.ParseOpenMenu(evt));
                            break;
                        case EventType.OpenSave:
                            sequence.Add(XmlSceneControlParser.ParseOpenSave(evt));
                            break;
                        case EventType.GameOver:
                            sequence.Add(XmlSceneControlParser.ParseGameOver(evt));
                            break;
                        case EventType.ReturnToTitle:
                            sequence.Add(XmlSceneControlParser.ParseReturnToTitle(evt));
                            break;
                        case EventType.ChangeBattleBgm:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeBattleBgm(evt));
                            break;
                        case EventType.ChangeBattleEndMusicalEffect:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeBattleEndMusicalEffect(evt));
                            break;
                        case EventType.ChangeSaveAccess:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeSaveAccess(evt));
                            break;
                        case EventType.ChangeMenuAccess:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeMenuAccess(evt));
                            break;
                        case EventType.ChangeEncounterAccess:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeEncounterAccess(evt));
                            break;
                        case EventType.ChangeFormationAccess:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeFormationAccess(evt));
                            break;
                        case EventType.ChangeWindowColor:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeWindowColor(evt));
                            break;
                        case EventType.ChangeActorGraphic:
                            sequence.Add(XmlSystemSettingsParser.ParseChangeActorGraphic(evt));
                            break;
                        case EventType.ChangeMapNameDisplay:
                            sequence.Add(XmlMapParser.ParseChangeMapNameDisplay(evt));
                            break;
                        default:
                            throw new ArgumentException("Cannot resolve EventType " + eventType + ".");
                    }
                }
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("EventsSequenceParser cannot parse sequence. Exception: " + e.Message);
            }

            return sequence;
        }

        public static EventsSequence ParseEventsSequence(TextAsset file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(file.text);

                XmlNode eventsSequenceNode = document.SelectSingleNode("EventsSequence");

                return ParseEventsSequence(eventsSequenceNode);
            }
            catch (Exception e)
            {
                LogsHandler.Instance.LogFatalError("EventsSequenceParser cannot parse sequence. Exception: " + e.Message);
                return new EventsSequence();
            }
        }
    }
}
