using NUnit.Framework;
using Engine.Character;

namespace Testing.Engine.Character
{
    public class XmlCharacterParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Character/TestData.xml"; } }

        [Test]
        public void ParseShowAnimationTest()
        {
            ShowAnimation show = XmlCharacterParser.ParseShowAnimation(GetDataToParse("ShowAnimation"));

            Assert.AreEqual("target", show.Target);
            Assert.AreEqual(3, show.AnimationId);
            Assert.AreEqual(false, show.WaitForCompletion);
        }

        [Test]
        public void ParseShowAgentAnimationTest()
        {
            ShowAgentAnimation show = XmlCharacterParser.ParseShowAgentAnimation(GetDataToParse("ShowAgentAnimation"));

            Assert.AreEqual("animation", show.AnimationName);
            Assert.AreEqual(false, show.WaitForCompletion);
        }

        [Test]
        public void ShowBalloonIconTest()
        {
            ShowBalloonIcon show = XmlCharacterParser.ParseShowBalloonIcon(GetDataToParse("ShowBalloonIcon"));

            Assert.AreEqual("target", show.Target);
            Assert.AreEqual(5, show.BalloonIconId);
            Assert.AreEqual(true, show.WaitForCompletion);
        }
    }
}
