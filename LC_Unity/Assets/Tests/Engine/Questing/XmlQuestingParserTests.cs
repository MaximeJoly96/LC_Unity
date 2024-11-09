using Engine.Questing;
using Questing;
using NUnit.Framework;

namespace Testing.Engine.Questing
{
    public class XmlQuestingParserTests : XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/Questing/TestData/TestData.xml"; } }

        [Test]
        public void StartQuestCanBeParsed()
        {
            StartQuest start = XmlQuestingParser.ParseStartQuest(GetDataToParse("StartQuest"));

            Assert.AreEqual(3, start.Id);
        }

        [Test]
        public void FailQuestCanBeParsed()
        {
            FailQuest fail = XmlQuestingParser.ParseFailQuest(GetDataToParse("FailQuest"));

            Assert.AreEqual(5, fail.Id);
        }

        [Test]
        public void CompleteQuestCanBeParsed()
        {
            CompleteQuest complete = XmlQuestingParser.ParseCompleteQuest(GetDataToParse("CompleteQuest"));

            Assert.AreEqual(18, complete.Id);
        }

        [Test]
        public void ProgressQuestCanBeParsed()
        {
            ProgressQuest progress = XmlQuestingParser.ParseProgressQuest(GetDataToParse("ProgressQuest"));

            Assert.AreEqual(7, progress.Id);
            Assert.AreEqual(9, progress.StepId);
            Assert.AreEqual(QuestStepStatus.Failed, progress.StepStatus);
        }
    }
}
