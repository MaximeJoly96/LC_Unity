using Engine.GameProgression;
using NUnit.Framework;

namespace Testing.Engine.GameProgession
{
    public class XmlGameProgressionParserTests: XmlBaseParser
    {
        protected override string TestFilePath { get { return "Assets/Tests/Engine/GameProgression/TestData.xml"; } }

        [Test]
        public void ParseControlSwitchTest()
        {
            ControlSwitch control = XmlGameProgressionParser.ParseControlSwitch(GetDataToParse("ControlSwitch", 0));

            Assert.AreEqual("switch1", control.Key);
            Assert.AreEqual("switch2", control.Source);

            control = XmlGameProgressionParser.ParseControlSwitch(GetDataToParse("ControlSwitch", 1));

            Assert.AreEqual(false, control.Value);
        }

        [Test]
        public void ParseControlVariableTest()
        {
            ControlVariable control = XmlGameProgressionParser.ParseControlVariable(GetDataToParse("ControlVariable", 0));

            Assert.AreEqual("var1", control.Key);
            Assert.AreEqual(ControlVariable.Operator.Add, control.Operation);
            Assert.AreEqual(ControlVariable.OperandType.Constant, control.Operand);
            Assert.AreEqual(5, control.Values[0]);

            control = XmlGameProgressionParser.ParseControlVariable(GetDataToParse("ControlVariable", 1));

            Assert.AreEqual(ControlVariable.Operator.Sub, control.Operation);
            Assert.AreEqual(ControlVariable.OperandType.Random, control.Operand);
            Assert.AreEqual(2, control.Values[0]);
            Assert.AreEqual(6, control.Values[1]);

            control = XmlGameProgressionParser.ParseControlVariable(GetDataToParse("ControlVariable", 2));

            Assert.AreEqual(ControlVariable.Operator.Mod, control.Operation);
            Assert.AreEqual(ControlVariable.OperandType.Variable, control.Operand);
            Assert.AreEqual("var2", control.Source);
        }

        [Test]
        public void ParseControlTimerTest()
        {
            ControlTimer control = XmlGameProgressionParser.ParseControlTimer(GetDataToParse("ControlTimer", 0));

            Assert.AreEqual("timer1", control.Key);
            Assert.AreEqual(ControlTimer.TimerAction.Start, control.Action);
            Assert.AreEqual(10, control.Duration);

            control = XmlGameProgressionParser.ParseControlTimer(GetDataToParse("ControlTimer", 1));

            Assert.AreEqual("timer2", control.Source);

            control = XmlGameProgressionParser.ParseControlTimer(GetDataToParse("ControlTimer", 2));

            Assert.AreEqual(ControlTimer.TimerAction.Stop, control.Action);
        }
    }
}
