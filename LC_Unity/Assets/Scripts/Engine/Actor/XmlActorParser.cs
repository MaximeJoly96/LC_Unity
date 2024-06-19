using System.Xml;
using System;

namespace Engine.Actor
{
    public static class XmlActorParser
    {
        public static ChangeEquipment ParseChangeEquipment(XmlNode data)
        {
            ChangeEquipment change = new ChangeEquipment();

            change.CharacterId = int.Parse(data.Attributes["CharacterId"].InnerText);
            change.ItemId = int.Parse(data.Attributes["ItemId"].InnerText);

            return change;
        }

        public static ChangeExp ParseChangeExp(XmlNode data)
        {
            ChangeExp change = new ChangeExp();

            change.Amount = int.Parse(data.Attributes["Amount"].InnerText);
            change.CharacterId = int.Parse(data.Attributes["Target"].InnerText);

            return change;
        }

        public static ChangeLevel ParseChangeLevel(XmlNode data)
        {
            ChangeLevel change = new ChangeLevel();

            change.Amount = int.Parse(data.Attributes["Amount"].InnerText);
            change.CharacterId = int.Parse(data.Attributes["Target"].InnerText);

            return change;
        }

        public static ChangeName ParseChangeName(XmlNode data)
        {
            ChangeName change = new ChangeName();

            change.CharacterId = int.Parse(data.Attributes["CharacterId"].InnerText);
            change.Value = data.Attributes["Value"].InnerText;

            return change;
        }

        public static ChangeSkills ParseChangeSkills(XmlNode data)
        {
            ChangeSkills change = new ChangeSkills();

            change.CharacterId = int.Parse(data.Attributes["CharacterId"].InnerText);
            change.SkillId = int.Parse(data.Attributes["SkillId"].InnerText);
            change.Action = (ChangeSkills.ActionType)Enum.Parse(typeof(ChangeSkills.ActionType), data.Attributes["Action"].InnerText);

            return change;
        }

        public static RecoverAll ParseRecoverAll(XmlNode data)
        {
            RecoverAll recover = new RecoverAll();

            return recover;
        }
    }
}
