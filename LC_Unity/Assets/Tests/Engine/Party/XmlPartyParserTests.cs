using Engine.Party;
using NUnit.Framework;

namespace Testing.Engine.Party
{
    public class XmlPartyParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Party/TestData.xml"; } }

        [Test]
        public void ParseChangeGoldTest()
        {
            ChangeGold change = XmlPartyParser.ParseChangeGold(GetDataToParse("ChangeGold"));

            Assert.AreEqual(500, change.Value);
        }

        [Test]
        public void ParseChangeItemsTest()
        {
            ChangeItems change = XmlPartyParser.ParseChangeItems(GetDataToParse("ChangeItems"));

            Assert.AreEqual(3, change.Id);
            Assert.AreEqual(2, change.Quantity);
        }

        [Test]
        public void ParseChangePartyMemberTest()
        {
            ChangePartyMember change = XmlPartyParser.ParseChangePartyMember(GetDataToParse("ChangePartyMember", 0));

            Assert.AreEqual(0, change.Id);
            Assert.AreEqual(ChangePartyMember.ActionType.Remove, change.Action);

            change = XmlPartyParser.ParseChangePartyMember(GetDataToParse("ChangePartyMember", 1));

            Assert.AreEqual(ChangePartyMember.ActionType.Add, change.Action);
            Assert.AreEqual(false, change.Initialize);
        }
    }
}
