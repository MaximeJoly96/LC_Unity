using Engine.PictureAndWeather;
using NUnit.Framework;
using UnityEngine;

namespace Testing.Engine.PictureAndWeather
{
    public class XmlPictureAndWeatherParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/PictureAndWeather/TestData.xml"; } }

        [Test]
        public void ParseMovePictureTest()
        {
            MovePicture move = XmlPictureAndWeatherParser.ParseMovePicture(GetDataToParse("MovePicture"));

            Assert.AreEqual(0, move.Id);
            Assert.AreEqual(200, move.X);
            Assert.AreEqual(150, move.Y);
            Assert.IsTrue(Mathf.Abs(1 - move.Alpha) < 0.001f);
            Assert.AreEqual(7, move.Duration);
            Assert.AreEqual(false, move.WaitForCompletion);
        }

        [Test]
        public void ParseRotatePictureTest()
        {
            RotatePicture rotate = XmlPictureAndWeatherParser.ParseRotatePicture(GetDataToParse("RotatePicture"));

            Assert.AreEqual(1, rotate.Id);
            Assert.AreEqual(45, rotate.Angle);
            Assert.AreEqual(2, rotate.Duration);
        }

        [Test]
        public void ParseSetWeatherEffectsTest()
        {
            SetWeatherEffects set = XmlPictureAndWeatherParser.ParseSetWeatherEffects(GetDataToParse("SetWeatherEffects"));

            Assert.AreEqual(SetWeatherEffects.WeatherType.Snow, set.Weather);
            Assert.AreEqual(5, set.Power);
            Assert.AreEqual(0, set.TransitionDuration);
            Assert.AreEqual(true, set.WaitForCompletion);
        }

        [Test]
        public void ParseShowPictureTest()
        {
            ShowPicture show = XmlPictureAndWeatherParser.ParseShowPicture(GetDataToParse("ShowPicture", 0));

            Assert.AreEqual(1, show.Id);
            Assert.AreEqual(false, show.Show);

            show = XmlPictureAndWeatherParser.ParseShowPicture(GetDataToParse("ShowPicture", 1));

            Assert.AreEqual("graphic", show.Graphic);
            Assert.AreEqual(-150, show.X);
            Assert.AreEqual(-200, show.Y);
            Assert.IsTrue(Mathf.Abs(0.65f - show.Alpha) < 0.001f);
        }

        [Test]
        public void ParseTintPictureTest()
        {
            TintPicture tint = XmlPictureAndWeatherParser.ParseTintPicture(GetDataToParse("TintPicture"));

            Assert.AreEqual(3, tint.Id);
            Assert.AreEqual(7, tint.Duration);
            Assert.AreEqual(true, tint.WaitForCompletion);
            Assert.IsTrue(Mathf.Abs(0.4f - tint.TargetColor.r) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.3f - tint.TargetColor.g) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.75f - tint.TargetColor.b) < 0.001f);
        }
    }
}
