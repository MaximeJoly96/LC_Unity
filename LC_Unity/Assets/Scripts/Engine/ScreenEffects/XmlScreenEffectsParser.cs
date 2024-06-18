using System;
using Logging;
using System.Xml;
using UnityEngine;
using System.Globalization;

namespace Engine.ScreenEffects
{
    public static class XmlScreenEffectsParser
    {
        public static FadeScreen ParseFadeScreen(XmlNode data)
        {
            FadeScreen fade = new FadeScreen();

            try
            {
                fade.FadeIn = bool.Parse(data.Attributes["FadeIn"].InnerText);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlScreenEffectsParser cannot parse FadeScreen. Exception: " + e.Message);
            }

            return fade;
        }

        public static TintScreen ParseTintScreen(XmlNode data)
        {
            TintScreen tint = new TintScreen();

            try
            {
                tint.Duration = float.Parse(data.Attributes["Duration"].InnerText, CultureInfo.InvariantCulture);
                tint.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

                XmlNode colorNode = data.SelectSingleNode("TargetColor");
                tint.TargetColor = new Color(float.Parse(colorNode.Attributes["R"].InnerText, CultureInfo.InvariantCulture),
                                             float.Parse(colorNode.Attributes["G"].InnerText, CultureInfo.InvariantCulture),
                                             float.Parse(colorNode.Attributes["B"].InnerText, CultureInfo.InvariantCulture),
                                             float.Parse(colorNode.Attributes["A"].InnerText, CultureInfo.InvariantCulture));
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlScreenEffectsParser cannot parse TintScreen. Exception: " + e.Message);
            }

            return tint;
        }

        public static FlashScreen ParseFlashScreen(XmlNode data)
        {
            FlashScreen flash = new FlashScreen();

            try
            {
                flash.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

                XmlNode colorNode = data.SelectSingleNode("TargetColor");
                flash.TargetColor = new Color(float.Parse(colorNode.Attributes["R"].InnerText, CultureInfo.InvariantCulture),
                                              float.Parse(colorNode.Attributes["G"].InnerText, CultureInfo.InvariantCulture),
                                              float.Parse(colorNode.Attributes["B"].InnerText, CultureInfo.InvariantCulture),
                                              float.Parse(colorNode.Attributes["A"].InnerText, CultureInfo.InvariantCulture));
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlScreenEffectsParser cannot parse FlashScreen. Exception: " + e.Message);
            }

            return flash;
        }

        public static ShakeScreen ParseShakeScreen(XmlNode data)
        {
            ShakeScreen shake = new ShakeScreen();

            try
            {
                shake.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);
                shake.Power = int.Parse(data.Attributes["Power"].InnerText);
                shake.Speed = int.Parse(data.Attributes["Speed"].InnerText);
                shake.Duration = int.Parse(data.Attributes["Duration"].InnerText);
            }
            catch(Exception e)
            {
                LogsHandler.Instance.LogFatalError("XmlScreenEffectsParser cannot parse ShakeScreen. Exception: " + e.Message);
            }

            return shake;
        }
    }
}
