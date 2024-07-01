using System.Xml;
using System;

namespace Engine.Character
{
    public static class XmlCharacterParser
    {
        public static ShowAnimation ParseShowAnimation(XmlNode data)
        {
            ShowAnimation show = new ShowAnimation();

            show.Target = data.Attributes["Target"].InnerText;
            show.AnimationId = int.Parse(data.Attributes["AnimationId"].InnerText);
            show.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return show;
        }

        public static ShowBalloonIcon ParseShowBalloonIcon(XmlNode data)
        {
            ShowBalloonIcon show = new ShowBalloonIcon();

            show.Target = data.Attributes["Target"].InnerText;
            show.BalloonIconId = int.Parse(data.Attributes["BalloonIconId"].InnerText);
            show.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return show;
        }

        public static ShowAgentAnimation ParseShowAgentAnimation(XmlNode data)
        {
            ShowAgentAnimation show = new ShowAgentAnimation();

            show.AnimationName = data.Attributes["AnimationName"].InnerText;
            show.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return show;
        }
    }
}
