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

        [Test]
        public void GlobalStateMachineSpecificTransitionsTest()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField);
            Assert.AreEqual(GlobalStateMachine.State.OnField, GlobalStateMachine.Instance.CurrentState);

            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuSystemTab);
            Assert.AreEqual(GlobalStateMachine.State.OnField, GlobalStateMachine.Instance.CurrentState);
        }

        [Test]
        public void StateCanBeRememberedAndReloaded()
        {
            GlobalStateMachine machine = GlobalStateMachine.Instance;

            machine.UpdateState(GlobalStateMachine.State.OnField);
            machine.RememberState();

            Assert.AreEqual(GlobalStateMachine.State.OnField, machine.CurrentState);

            machine.UpdateState(GlobalStateMachine.State.ClosingSaves);

            Assert.AreEqual(GlobalStateMachine.State.ClosingSaves, machine.CurrentState);

            machine.LoadRememberedState();

            Assert.AreEqual(GlobalStateMachine.State.OnField, machine.CurrentState);
        }
    }
}
