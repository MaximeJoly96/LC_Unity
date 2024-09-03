using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Engine.Actor;
using System.Xml;
using UnityEditor;
using Engine.Events;
using static Engine.Actor.ChangeSkills;

namespace Testing.Engine.Actor
{
    public class XmlActorParserTests
    {
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

        private XmlNode GetDataToParse(string type)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/Engine/Actor/TestData.xml").text);

            XmlNode sequence = document.SelectSingleNode("EventsSequence");
            return sequence.SelectSingleNode(type);
        }
    }
}
