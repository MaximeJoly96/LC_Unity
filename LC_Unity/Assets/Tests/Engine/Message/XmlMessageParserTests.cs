using Engine.Message;
using NUnit.Framework;

namespace Testing.Engine.Message
{
    public class XmlMessageParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Message/TestData.xml"; } }

        [Test]
        public void ParseDisplayDialogTests()
        {
            DisplayDialog dialog = XmlMessageParser.ParseDialogData(GetDataToParse("DisplayDialog"));

            Assert.AreEqual(DialogBoxPosition.Bottom, dialog.BoxPosition);
            Assert.AreEqual(DialogBoxStyle.Classic, dialog.BoxStyle);
            Assert.AreEqual("Locutor", dialog.Locutor);
            Assert.AreEqual("This is my message", dialog.Message);
            Assert.AreEqual("graphics", dialog.FaceGraphics);
        }

        [Test]
        public void ParseDisplayChoiceListTest()
        {
            DisplayChoiceList choiceList = XmlMessageParser.ParseChoiceListData(GetDataToParse("DisplayChoiceList"));

            Assert.AreEqual("Choice message", choiceList.Message);
            Assert.AreEqual(3, choiceList.Choices.Count);
            Assert.AreEqual("Choice 0", choiceList.Choices[0].Text);
            Assert.AreEqual("0", choiceList.Choices[0].Id);
            Assert.AreEqual("Choice 1", choiceList.Choices[1].Text);
            Assert.AreEqual("1", choiceList.Choices[1].Id);
            Assert.AreEqual("Choice 2", choiceList.Choices[2].Text);
            Assert.AreEqual("2", choiceList.Choices[2].Id);
        }

        [Test]
        public void ParseDisplayInputNumberTest()
        {
            DisplayInputNumber input = XmlMessageParser.ParseInputNumberData(GetDataToParse("DisplayInputNumber"));

            Assert.AreEqual(4, input.DigitsCount);
        }
    }
}
