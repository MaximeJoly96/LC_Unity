using System.Xml;
using System;
using UnityEngine;

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

            change.TargetColor = new Color(float.Parse(data.Attributes["R"].InnerText),
                                           float.Parse(data.Attributes["G"].InnerText),
                                           float.Parse(data.Attributes["B"].InnerText));

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
    }
}
