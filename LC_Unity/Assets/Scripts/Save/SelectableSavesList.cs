using UI;
using Core;

namespace Save
{
    public class SelectableSavesList : SelectableList
    {
        protected override bool CanReceiveInputs()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BrowsingSaves;
        }
    }
}
