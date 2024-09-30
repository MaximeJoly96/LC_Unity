using BattleSystem.Behaviours.AiBehaviours;
using NUnit.Framework;

namespace Testing.BattleSystem.Behaviours.AiBehaviours
{
    public class AiScriptTests
    {
        [Test]
        public void SetMainConditionTest()
        {
            AiScript script = new AiScript();

            Assert.IsNull(script.MainCondition);

            script.SetMainCondition(new DefaultCondition());

            Assert.IsNotNull(script.MainCondition);
        }
    }
}
