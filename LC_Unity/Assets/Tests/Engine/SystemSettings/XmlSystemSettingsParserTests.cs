using Engine.SystemSettings;
using NUnit.Framework;
using UnityEngine;

namespace Testing.Engine.SystemSettings
{
    public class XmlSystemSettingsParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/SystemSettings/TestData.xml"; } }

        [Test]
        public void ParseChangeBattleBgmTest()
        {
            ChangeBattleBgm change = XmlSystemSettingsParser.ParseChangeBattleBgm(GetDataToParse("ChangeBattleBgm"));

            Assert.AreEqual("battleMusic", change.Name);
            Assert.AreEqual(50, change.Volume);
            Assert.AreEqual(100, change.Pitch);
        }

        [Test]
        public void ParseChangeBattleEndMusicalEffectTest()
        {
            ChangeBattleEndMusicalEffect change = XmlSystemSettingsParser.ParseChangeBattleEndMusicalEffect(GetDataToParse("ChangeBattleEndMusicalEffect"));

            Assert.AreEqual("battleEnd", change.Name);
            Assert.AreEqual(75, change.Volume);
            Assert.AreEqual(75, change.Pitch);
        }

        [Test]
        public void ParseChangeSaveAccessTest()
        {
            ChangeSaveAccess change = XmlSystemSettingsParser.ParseChangeSaveAccess(GetDataToParse("ChangeSaveAccess"));

            Assert.AreEqual(false, change.Enabled);
        }

        [Test]
        public void ParseChangeMenuAccessTest()
        {
            ChangeMenuAccess change = XmlSystemSettingsParser.ParseChangeMenuAccess(GetDataToParse("ChangeMenuAccess"));

            Assert.AreEqual(true, change.Enabled);
        }

        [Test]
        public void ParseChangeEncounterAccessTest()
        {
            ChangeEncounterAccess change = XmlSystemSettingsParser.ParseChangeEncounterAccess(GetDataToParse("ChangeEncounterAccess"));

            Assert.AreEqual(false, change.Enabled);
        }

        [Test]
        public void ParseChangeFormationTest()
        {
            ChangeFormationAccess change = XmlSystemSettingsParser.ParseChangeFormationAccess(GetDataToParse("ChangeFormationAccess"));

            Assert.AreEqual(true, change.Enabled);
        }

        [Test]
        public void ParseChangeWindowColorTest()
        {
            ChangeWindowColor change = XmlSystemSettingsParser.ParseChangeWindowColor(GetDataToParse("ChangeWindowColor"));

            Assert.IsTrue(Mathf.Abs(0.5f - change.TargetColor.r) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.2f - change.TargetColor.g) < 0.001f);
            Assert.IsTrue(Mathf.Abs(0.8f - change.TargetColor.b) < 0.001f);
        }

        [Test]
        public void ParseChangeActorGraphicTest()
        {
            ChangeActorGraphic change = XmlSystemSettingsParser.ParseChangeActorGraphic(GetDataToParse("ChangeActorGraphic"));

            Assert.AreEqual(0, change.CharacterId);
            Assert.AreEqual("charset", change.Charset);
            Assert.AreEqual("faceset", change.Faceset);
        }

        [Test]
        public void ParseAllowCutsceneSkipTest()
        {
            AllowCutsceneSkip allow = XmlSystemSettingsParser.ParseAllowCutsceneSkip(GetDataToParse("AllowCutsceneSkip"));

            Assert.IsTrue(allow.Allow);
            Assert.AreEqual(1, allow.ActionsWhenSkipping.Events.Count);
        }

        [Test]
        public void ParseChangeGameStateTest()
        {
            ChangeGameState change = XmlSystemSettingsParser.ParseChangeGameState(GetDataToParse("ChangeGameState"));

            Assert.AreEqual("OnField", change.State);
        }
    }
}
