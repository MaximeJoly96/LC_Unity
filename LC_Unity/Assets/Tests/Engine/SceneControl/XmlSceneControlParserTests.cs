using NUnit.Framework;
using Engine.SceneControl;

namespace Testing.Engine.SceneControl
{
    public class XmlSceneControlParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/SceneControl/TestData.xml"; } }

        [Test]
        public void ParseBattleProcessingTest()
        {
            BattleProcessing battle = XmlSceneControlParser.ParseBattleProcessing(GetDataToParse("BattleProcessing", 0));

            Assert.AreEqual(false, battle.FromRandomEncounter);
            Assert.AreEqual(3, battle.TroopId);
            Assert.AreEqual(true, battle.CanEscape);
            Assert.AreEqual(true, battle.DefeatAllowed);

            battle = XmlSceneControlParser.ParseBattleProcessing(GetDataToParse("BattleProcessing", 1));

            Assert.AreEqual(true, battle.FromRandomEncounter);
        }

        [Test]
        public void ParseShopProcessingTest()
        {
            ShopProcessing shop = XmlSceneControlParser.ParseShopProcessing(GetDataToParse("ShopProcessing"));

            Assert.AreEqual(2, shop.MerchantId);
        }

        [Test]
        public void ParseNameInputProcessingTest()
        {
            NameInputProcessing name = XmlSceneControlParser.ParseNameInputProcessing(GetDataToParse("NameInputProcessing"));

            Assert.AreEqual(0, name.CharacterId);
            Assert.AreEqual(16, name.MaxCharacters);
        }

        [Test]
        public void ParseOpenMenuTest()
        {
            OpenMenu open = XmlSceneControlParser.ParseOpenMenu(GetDataToParse("OpenMenu"));

            Assert.NotNull(open);
        }

        [Test]
        public void ParseOpenSaveTest()
        {
            OpenSave open = XmlSceneControlParser.ParseOpenSave(GetDataToParse("OpenSave"));

            Assert.NotNull(open);
        }

        [Test]
        public void ParseGameOverTest()
        {
            GameOver gameOver = XmlSceneControlParser.ParseGameOver(GetDataToParse("GameOver"));

            Assert.NotNull(gameOver);
        }

        [Test]
        public void ParseReturnToTitleTest()
        {
            ReturnToTitle returnToTitle = XmlSceneControlParser.ParseReturnToTitle(GetDataToParse("ReturnToTitle"));

            Assert.NotNull(returnToTitle);
        }
    }
}
