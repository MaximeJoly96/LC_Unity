using Engine.ScreenEffects;
using NUnit.Framework;
using UnityEngine;

namespace Testing.Engine.ScreenEffects
{
    public class XmlScreenEffectsParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/ScreenEffects/TestData.xml"; } }

        [Test]
        public void ParseFadeScreenTest()
        {
            FadeScreen fade = XmlScreenEffectsParser.ParseFadeScreen(GetDataToParse("FadeScreen"));

            Assert.AreEqual(true, fade.FadeIn);
        }

        [Test]
        public void ParseTintScreenTest()
        {
            TintScreen tint = XmlScreenEffectsParser.ParseTintScreen(GetDataToParse("TintScreen"));

            Assert.IsTrue(Mathf.Abs(3.5f - tint.Duration) < 0.001f);
            Assert.AreEqual(true, tint.WaitForCompletion);
            Assert.IsTrue(Mathf.Abs(0.5f - tint.TargetColor.r) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.35f - tint.TargetColor.g) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.83f - tint.TargetColor.b) < 0.001f);
            Assert.IsTrue(Mathf.Abs(1.0f - tint.TargetColor.a) < 0.001f);
        }

        [Test]
        public void ParseFlashScreenTest()
        {
            FlashScreen flash = XmlScreenEffectsParser.ParseFlashScreen(GetDataToParse("FlashScreen"));

            Assert.AreEqual(false, flash.WaitForCompletion);
            Assert.IsTrue(Mathf.Abs(0.92f - flash.TargetColor.r) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.25f - flash.TargetColor.g) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.18f - flash.TargetColor.b) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.5f - flash.TargetColor.a) < 0.001f);
        }
    }
}
