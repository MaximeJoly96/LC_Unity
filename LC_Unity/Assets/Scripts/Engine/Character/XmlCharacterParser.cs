﻿using System.Xml;
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

            show.AgentId = data.Attributes["AgentId"].InnerText;
            show.BalloonIcon = (ShowBalloonIcon.BalloonType)Enum.Parse(typeof(ShowBalloonIcon.BalloonType), 
                                                                       data.Attributes["BalloonIcon"].InnerText);
            show.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return show;
        }

        public static ShowAgentAnimation ParseShowAgentAnimation(XmlNode data)
        {
            ShowAgentAnimation show = new ShowAgentAnimation();

            show.AnimationName = data.Attributes["AnimationName"].InnerText;
            show.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);
            show.Target = data.Attributes["Target"].InnerText;

            return show;
        }

        public static DisableAgent ParseDisableAgent(XmlNode data)
        {
            DisableAgent disable = new DisableAgent();

            disable.Target = data.Attributes["Target"].InnerText;

            return disable;
        }

        public static EnableAgent ParseEnableAgent(XmlNode data)
        {
            EnableAgent enable = new EnableAgent();

            enable.Target = data.Attributes["Target"].InnerText;

            return enable;
        }

        public static ResetAgentAnimationState ParseResetAgentAnimationState(XmlNode data)
        {
            ResetAgentAnimationState reset = new ResetAgentAnimationState();

            reset.Target = data.Attributes["Target"].InnerText;

            return reset;
        }
    }
}
