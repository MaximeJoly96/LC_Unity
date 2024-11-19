using UI;
using Core;
using System.Collections.Generic;
using Save.Model;

namespace Save
{
    public class SelectableSavesList : SelectableList
    {
        protected override bool CanReceiveInputs()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.BrowsingSaves;
        }

        public void FeedSaves(List<SaveDescriptor> saves)
        {
            Clear();

            for(int i = 0; i < saves.Count; i++)
            {
                SaveSlot slot = AddItem() as SaveSlot;
                slot.Init(saves[i]);
            }
        }
    }
}
