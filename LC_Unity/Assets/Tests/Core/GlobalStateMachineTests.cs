using NUnit.Framework;
using Core;

namespace Testing.Core
{
    public class GlobalStateMachineTests
    {
        [Test]
        public void GlobalStateMachineUpdateStateTest()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenu);

            Assert.AreEqual(GlobalStateMachine.State.InMenu, GlobalStateMachine.Instance.CurrentState);
        }

        public void GlobalStateMachineSpecificTransitionsTest()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
            Assert.AreEqual(GlobalStateMachine.State.OnField, GlobalStateMachine.Instance.CurrentState);

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
            Assert.AreEqual(GlobalStateMachine.State.OnField, GlobalStateMachine.Instance.CurrentState);
        }
    }
}
