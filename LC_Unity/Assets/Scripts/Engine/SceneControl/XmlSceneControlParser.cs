using System;
using System.Xml;

namespace Engine.SceneControl
{
    public static class XmlSceneControlParser
    {
        public static BattleProcessing ParseBattleProcessing(XmlNode data)
        {
            BattleProcessing battle = new BattleProcessing();

            battle.FromRandomEncounter = bool.Parse(data.Attributes["FromRandomEncounter"].InnerText);
            if (!battle.FromRandomEncounter)
                battle.TroopId = int.Parse(data.Attributes["TroopId"].InnerText);

            battle.CanEscape = bool.Parse(data.Attributes["CanEscape"].InnerText);
            battle.DefeatAllowed = bool.Parse(data.Attributes["DefeatAllowed"].InnerText);

            return battle;
        }

        public static ShopProcessing ParseShopProcessing(XmlNode data)
        {
            ShopProcessing shop = new ShopProcessing();

            shop.MerchantId = int.Parse(data.Attributes["MerchantId"].InnerText);

            return shop;
        }

        public static NameInputProcessing ParseNameInputProcessing(XmlNode data)
        {
            NameInputProcessing name = new NameInputProcessing();

            name.CharacterId = int.Parse(data.Attributes["CharacterId"].InnerText);
            name.MaxCharacters = int.Parse(data.Attributes["MaxCharacters"].InnerText);

            return name;
        }

        public static OpenMenu ParseOpenMenu(XmlNode data)
        {
            OpenMenu menu = new OpenMenu();

            return menu;
        }

        public static OpenSave ParseOpenSave(XmlNode data)
        {
            OpenSave save = new OpenSave();

            return save;
        }

        public static GameOver ParseGameOver(XmlNode data)
        {
            GameOver over = new GameOver();

            return over;
        }

        public static ReturnToTitle ParseReturnToTitle(XmlNode data)
        {
            ReturnToTitle title = new ReturnToTitle();

            return title;
        }
    }
}
