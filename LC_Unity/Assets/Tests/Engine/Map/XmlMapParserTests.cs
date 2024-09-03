using Engine.Map;
using NUnit.Framework;

namespace Testing.Engine.Map
{
    public class XmlMapParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Map/TestData.xml"; } }

        [Test]
        public void ParseChangeMapNameDisplayTest()
        {
            ChangeMapNameDisplay change = XmlMapParser.ParseChangeMapNameDisplay(GetDataToParse("ChangeMapNameDisplay"));

            Assert.AreEqual(true, change.Enabled);
        }
    }
}
