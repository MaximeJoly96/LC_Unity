using System;
using System.Xml;
using System.Globalization;

namespace Engine.MusicAndSounds
{
    public static class XmlMusicAndSoundsParser
    {
        public static PlayBgm ParsePlayBgm(XmlNode data)
        {
            PlayBgm play = new PlayBgm();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = float.Parse(data.Attributes["Volume"].InnerText, CultureInfo.InvariantCulture);
            play.Pitch = float.Parse(data.Attributes["Pitch"].InnerText, CultureInfo.InvariantCulture);

            return play;
        }

        public static FadeOutBgm ParseFadeOutBgm(XmlNode data)
        {
            FadeOutBgm fadeOut = new FadeOutBgm();

            fadeOut.Name = data.Attributes["Name"].InnerText;
            fadeOut.TransitionDuration = float.Parse(data.Attributes["TransitionDuration"].InnerText, CultureInfo.InvariantCulture);

            return fadeOut;
        }

        public static PlayBgs ParsePlayBgs(XmlNode data)
        {
            PlayBgs play = new PlayBgs();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = float.Parse(data.Attributes["Volume"].InnerText, CultureInfo.InvariantCulture);
            play.Pitch = float.Parse(data.Attributes["Pitch"].InnerText, CultureInfo.InvariantCulture);

            return play;
        }

        public static FadeOutBgs ParseFadeOutBgs(XmlNode data)
        {
            FadeOutBgs fadeOut = new FadeOutBgs();

            fadeOut.Name = data.Attributes["Name"].InnerText;
            fadeOut.TransitionDuration = float.Parse(data.Attributes["TransitionDuration"].InnerText, CultureInfo.InvariantCulture);

            return fadeOut;
        }

        public static PlayMusicalEffect ParsePlayMusicalEffect(XmlNode data)
        {
            PlayMusicalEffect play = new PlayMusicalEffect();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = float.Parse(data.Attributes["Volume"].InnerText, CultureInfo.InvariantCulture);
            play.Pitch = float.Parse(data.Attributes["Pitch"].InnerText, CultureInfo.InvariantCulture);

            return play;
        }

        public static PlaySoundEffect ParsePlaySoundEffect(XmlNode data)
        {
            PlaySoundEffect play = new PlaySoundEffect();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = float.Parse(data.Attributes["Volume"].InnerText, CultureInfo.InvariantCulture);
            play.Pitch = float.Parse(data.Attributes["Pitch"].InnerText, CultureInfo.InvariantCulture);

            return play;
        }
    }
}
