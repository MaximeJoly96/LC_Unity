using Engine.MusicAndSounds;
using NUnit.Framework;

namespace Testing.Engine.MusicAndSounds 
{
    public class XmlMusicAndSoundsParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/MusicAndSounds/TestData.xml"; } }

        [Test]
        public void ParsePlayBgmTest()
        {
            PlayBgm play = XmlMusicAndSoundsParser.ParsePlayBgm(GetDataToParse("PlayBgm"));

            Assert.AreEqual("music", play.Name);
            Assert.AreEqual(0.5f, play.Volume);
            Assert.AreEqual(0.52f, play.Pitch);
        }

        [Test]
        public void ParseFadeOutBgmTest()
        {
            FadeOutBgm fade = XmlMusicAndSoundsParser.ParseFadeOutBgm(GetDataToParse("FadeOutBgm"));

            Assert.AreEqual("music", fade.Name);
            Assert.AreEqual(3, fade.TransitionDuration);
        }

        [Test]
        public void ParsePlayBgsTest()
        {
            PlayBgs play = XmlMusicAndSoundsParser.ParsePlayBgs(GetDataToParse("PlayBgs"));

            Assert.AreEqual("bgs", play.Name);
            Assert.AreEqual(0.5f, play.Volume);
            Assert.AreEqual(0.52f, play.Pitch);
        }

        [Test]
        public void ParseFadeOutBgsTest()
        {
            FadeOutBgs fade = XmlMusicAndSoundsParser.ParseFadeOutBgs(GetDataToParse("FadeOutBgs"));

            Assert.AreEqual("bgs", fade.Name);
            Assert.AreEqual(4.5f, fade.TransitionDuration);
        }

        [Test]
        public void ParsePlayMusicalEffectTest()
        {
            PlayMusicalEffect play = XmlMusicAndSoundsParser.ParsePlayMusicalEffect(GetDataToParse("PlayMusicalEffect"));

            Assert.AreEqual("me", play.Name);
            Assert.AreEqual(0.5f, play.Volume);
            Assert.AreEqual(0.52f, play.Pitch);
        }

        [Test]
        public void ParsePlaySoundEffectTest()
        {
            PlaySoundEffect play = XmlMusicAndSoundsParser.ParsePlaySoundEffect(GetDataToParse("PlaySoundEffect"));

            Assert.AreEqual("se", play.Name);
            Assert.AreEqual(0.5f, play.Volume);
            Assert.AreEqual(0.52f, play.Pitch);
        }

        [Test]
        public void ParseStopAllAudioTest()
        {
            StopAllAudio stop = XmlMusicAndSoundsParser.ParseStopAllAudio(GetDataToParse("StopAllAudio"));

            Assert.NotNull(stop);
        }
    }
}
