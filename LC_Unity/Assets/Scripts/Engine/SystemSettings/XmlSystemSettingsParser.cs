using System.Xml;
using System.Globalization;
using UnityEngine;
using Codice.Client.Common.TreeGrouper;
using Engine.Events;

namespace Engine.SystemSettings
{
    public static class XmlSystemSettingsParser
    {
        public static ChangeBattleBgm ParseChangeBattleBgm(XmlNode data)
        {
            ChangeBattleBgm change = new ChangeBattleBgm();

            change.Name = data.Attributes["Name"].InnerText;
            change.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            change.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return change;
        }

        public static ChangeBattleEndMusicalEffect ParseChangeBattleEndMusicalEffect(XmlNode data)
        {
            ChangeBattleEndMusicalEffect effect = new ChangeBattleEndMusicalEffect();

            effect.Name = data.Attributes["Name"].InnerText;
            effect.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            effect.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return effect;
        }

        public static ChangeSaveAccess ParseChangeSaveAccess(XmlNode data)
        {
            ChangeSaveAccess access = new ChangeSaveAccess();

            access.Enabled = bool.Parse(data.Attributes["Enabled"].InnerText);

            return access;
        }

        public static ChangeMenuAccess ParseChangeMenuAccess(XmlNode data)
        {
            ChangeMenuAccess access = new ChangeMenuAccess();

            access.Enabled = bool.Parse(data.Attributes["Enabled"].InnerText);

            return access;
        }

        public static ChangeEncounterAccess ParseChangeEncounterAccess(XmlNode data)
        {
            ChangeEncounterAccess access = new ChangeEncounterAccess();

            access.Enabled = bool.Parse(data.Attributes["Enabled"].InnerText);

            return access;
        }

        public static ChangeFormationAccess ParseChangeFormationAccess(XmlNode data)
        {
            ChangeFormationAccess access = new ChangeFormationAccess();

            access.Enabled = bool.Parse(data.Attributes["Enabled"].InnerText);

            return access;
        }

        public static ChangeWindowColor ParseChangeWindowColor(XmlNode data)
        {
            ChangeWindowColor change = new ChangeWindowColor();

            change.TargetColor = new Color(float.Parse(data.Attributes["R"].InnerText, CultureInfo.InvariantCulture),
                                           float.Parse(data.Attributes["G"].InnerText, CultureInfo.InvariantCulture),
                                           float.Parse(data.Attributes["B"].InnerText, CultureInfo.InvariantCulture));

            return change;
        }

        public static ChangeActorGraphic ParseChangeActorGraphic(XmlNode data)
        {
            ChangeActorGraphic change = new ChangeActorGraphic();

            change.CharacterId = int.Parse(data.Attributes["CharacterId"].InnerText);
            change.Charset = data.Attributes["Charset"].InnerText;
            change.Faceset = data.Attributes["Faceset"].InnerText;

            return change;
        }

        public static AllowCutsceneSkip ParseAllowCutsceneSkip(XmlNode data)
        {
            AllowCutsceneSkip allow = new AllowCutsceneSkip();

            allow.Allow = bool.Parse(data.Attributes["Allow"].InnerText);

            XmlNode actionsNode = data.SelectSingleNode("Actions");
            if (actionsNode != null)
            {
                allow.ActionsWhenSkipping = EventsSequenceParser.ParseEventsSequence(actionsNode);
            }

            return allow;
        }

        public static ChangeGameState ParseChangeGameState(XmlNode data)
        {
            ChangeGameState change = new ChangeGameState();

            change.State = data.Attributes["State"].InnerText;

            return change;
        }
    }
}
