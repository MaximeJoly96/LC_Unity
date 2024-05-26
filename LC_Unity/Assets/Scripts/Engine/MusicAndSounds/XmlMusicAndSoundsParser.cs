using System;
using System.Xml;

namespace Engine.MusicAndSounds
{
    public static class XmlMusicAndSoundsParser
    {
        public static PlayBgm ParsePlayBgm(XmlNode data)
        {
            PlayBgm play = new PlayBgm();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            play.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return play;
        }

        public static FadeOutBgm ParseFadeOutBgm(XmlNode data)
        {
            FadeOutBgm fadeOut = new FadeOutBgm();

            fadeOut.Name = data.Attributes["Name"].InnerText;
            fadeOut.TransitionDuration = int.Parse(data.Attributes["TransitionDuration"].InnerText);

            return fadeOut;
        }

        public static SaveBgm ParseSaveBgm(XmlNode data)
        {
            SaveBgm save = new SaveBgm();

            save.Name = data.Attributes["Name"].InnerText;

            return save;
        }

        public static ReplayBgm ParseReplayBgm(XmlNode data)
        {
            ReplayBgm save = new ReplayBgm();

            save.Name = data.Attributes["Name"].InnerText;

            return save;
        }

        public static PlayBgs ParsePlayBgs(XmlNode data)
        {
            PlayBgs play = new PlayBgs();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            play.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return play;
        }

        public static FadeOutBgs ParseFadeOutBgs(XmlNode data)
        {
            FadeOutBgs fadeOut = new FadeOutBgs();

            fadeOut.Name = data.Attributes["Name"].InnerText;
            fadeOut.TransitionDuration = int.Parse(data.Attributes["TransitionDuration"].InnerText);

            return fadeOut;
        }

        public static PlayMusicalEffect ParsePlayMusicalEffect(XmlNode data)
        {
            PlayMusicalEffect play = new PlayMusicalEffect();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            play.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return play;
        }

        public static PlaySoundEffect ParsePlaySoundEffect(XmlNode data)
        {
            PlaySoundEffect play = new PlaySoundEffect();

            play.Name = data.Attributes["Name"].InnerText;
            play.Volume = int.Parse(data.Attributes["Volume"].InnerText);
            play.Pitch = int.Parse(data.Attributes["Pitch"].InnerText);

            return play;
        }
    }
}
