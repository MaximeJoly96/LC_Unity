using NUnit.Framework;
using Engine.Actor;
using static Engine.Actor.ChangeSkills;

namespace Testing.Engine.Actor
{
    public class XmlActorParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Actor/TestData.xml"; } }

        [Test]
        public void ParseChangeEquipmentTest()
        {
            ChangeEquipment change = XmlActorParser.ParseChangeEquipment(GetDataToParse("ChangeEquipment"));

            Assert.AreEqual(0, change.CharacterId);
            Assert.AreEqual(3, change.ItemId);
        }

        [Test]
        public void ParseChangeExpTest()
        {
            ChangeExp change = XmlActorParser.ParseChangeExp(GetDataToParse("ChangeExp"));

            Assert.AreEqual(1, change.CharacterId);
            Assert.AreEqual(100, change.Amount);
        }

        [Test]
        public void ParseChangeLevelTest()
        {
            ChangeLevel change = XmlActorParser.ParseChangeLevel(GetDataToParse("ChangeLevel"));

            Assert.AreEqual(2, change.CharacterId);
            Assert.AreEqual(-1, change.Amount);
        }

        [Test]
        public void ParseChangeNameTest()
        {
            ChangeName change = XmlActorParser.ParseChangeName(GetDataToParse("ChangeName"));

            Assert.AreEqual(0, change.CharacterId);
            Assert.AreEqual("NewName", change.Value);
        }

        [Test]
        public void ParseChangeSkillsTest()
        {
            ChangeSkills change = XmlActorParser.ParseChangeSkills(GetDataToParse("ChangeSkills"));

            Assert.AreEqual(2, change.CharacterId);
            Assert.AreEqual(5, change.SkillId);
            Assert.AreEqual(ActionType.Learn, change.Action);
        }

        [Test]
        public void ParseRecoverAllTest()
        {
            RecoverAll recover = XmlActorParser.ParseRecoverAll(GetDataToParse("RecoverAll"));

            Assert.NotNull(recover);
        }
    }
}
