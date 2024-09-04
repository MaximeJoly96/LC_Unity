using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

namespace Engine.PictureAndWeather
{
    public static class XmlPictureAndWeatherParser
    {
        public static MovePicture ParseMovePicture(XmlNode data)
        {
            MovePicture move = new MovePicture();

            move.Id = int.Parse(data.Attributes["Id"].InnerText);
            move.X = int.Parse(data.Attributes["X"].InnerText);
            move.Y = int.Parse(data.Attributes["Y"].InnerText);
            move.Alpha = float.Parse(data.Attributes["Alpha"].InnerText, CultureInfo.InvariantCulture);
            move.Duration = int.Parse(data.Attributes["Duration"].InnerText);
            move.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return move;
        }

        public static RotatePicture ParseRotatePicture(XmlNode data)
        {
            RotatePicture rotate = new RotatePicture();

            rotate.Id = int.Parse(data.Attributes["Id"].InnerText);
            rotate.Angle = int.Parse(data.Attributes["Angle"].InnerText);
            rotate.Duration = int.Parse(data.Attributes["Duration"].InnerText);

            return rotate;
        }

        public static SetWeatherEffects ParseSetWeatherEffects(XmlNode data)
        {
            SetWeatherEffects weather = new SetWeatherEffects();

            weather.Weather = (SetWeatherEffects.WeatherType)Enum.Parse(typeof(SetWeatherEffects.WeatherType), data.Attributes["Weather"].InnerText);
            weather.Power = int.Parse(data.Attributes["Power"].InnerText);
            weather.TransitionDuration = int.Parse(data.Attributes["TransitionDuration"].InnerText);
            weather.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            return weather;
        }

        public static ShowPicture ParseShowPicture(XmlNode data)
        {
            ShowPicture show = new ShowPicture();

            show.Id = int.Parse(data.Attributes["Id"].InnerText);
            show.Show = bool.Parse(data.Attributes["Show"].InnerText);

            if(show.Show)
            {
                show.Graphic = data.Attributes["Graphic"].InnerText;
                show.X = int.Parse(data.Attributes["X"].InnerText);
                show.Y = int.Parse(data.Attributes["Y"].InnerText);
                show.Alpha = float.Parse(data.Attributes["Alpha"].InnerText, CultureInfo.InvariantCulture);
            }

            return show;
        }

        public static TintPicture ParseTintPicture(XmlNode data)
        {
            TintPicture tint = new TintPicture();

            tint.Id = int.Parse(data.Attributes["Id"].InnerText);
            tint.Duration = int.Parse(data.Attributes["Duration"].InnerText);
            tint.WaitForCompletion = bool.Parse(data.Attributes["WaitForCompletion"].InnerText);

            XmlNode colorNode = data.SelectSingleNode("TargetColor");
            tint.TargetColor = new Color(float.Parse(colorNode.Attributes["R"].InnerText, CultureInfo.InvariantCulture),
                                         float.Parse(colorNode.Attributes["G"].InnerText, CultureInfo.InvariantCulture),
                                         float.Parse(colorNode.Attributes["B"].InnerText, CultureInfo.InvariantCulture));

            return tint;
        }
    }
}
