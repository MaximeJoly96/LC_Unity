using Engine.Timing;
using NUnit.Framework;

namespace Testing.Engine.Timing
{
    public class XmlTimingParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Timing/TestData.xml"; } }

        [Test]
        public void ParseWaitTest()
        {
            Wait wait = XmlTimingParser.ParseWait(GetDataToParse("Wait"));

            Assert.AreEqual(32.5f, wait.Duration);
        }
    }
}
