using System.Xml;
using System;

namespace Engine.Party
{
    public static class XmlPartyParser
    {
        public static ChangeGold ParseChangeGold(XmlNode data)
        {
            ChangeGold change = new ChangeGold();

            change.Value = int.Parse(data.Attributes["Value"].InnerText);
            change.Notify = bool.Parse(data.Attributes["Notify"].InnerText);

            return change;
        }

        public static ChangeItems ParseChangeItems(XmlNode data)
        {
            ChangeItems change = new ChangeItems();

            change.Id = int.Parse(data.Attributes["Id"].InnerText);
            change.Quantity = int.Parse(data.Attributes["Quantity"].InnerText);
            change.Notify = bool.Parse(data.Attributes["Notify"].InnerText);

            return change;
        }

        public static ChangePartyMember ParseChangePartyMember(XmlNode data)
        {
            ChangePartyMember change = new ChangePartyMember();

            change.Id = int.Parse(data.Attributes["Id"].InnerText);
            change.Action = (ChangePartyMember.ActionType)Enum.Parse(typeof(ChangePartyMember.ActionType), data.Attributes["Action"].InnerText);
            change.Notify = bool.Parse(data.Attributes["Notify"].InnerText);

            if (change.Action == ChangePartyMember.ActionType.Add)
                change.Initialize = bool.Parse(data.Attributes["Initialize"].InnerText);

            return change;
        }
    }
}
