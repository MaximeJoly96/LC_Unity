using UnityEngine.Events;

namespace Core
{
    public class GlobalStateMachine
    {
        public enum State
        {
            OnField,
            InMenu,
            Interacting,
            ClosingMenu,
            OpeningMenu,
            SelectingCharacterPreview,
            InMenuItemsTab,
            InMenuEquipmentTab,
            InMenuAbilitiesTab,
            InMenuStatusTab,
            InMenuQuestsTab,
            InMenuSystemTab,
            TransitionCharacterTarget,
            InCharacterTargetMenu,
            OpeningMessageBox,
            InMessageBox,
            ClosingMessageBox
        }

        private static GlobalStateMachine _instance;

        public static GlobalStateMachine Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GlobalStateMachine();

                return _instance;
            }
        }

        public State CurrentState { get; private set; }
        public UnityEvent<State> StateChanged { get; private set; }
        public int CurrentMapId { get; set; }

        private GlobalStateMachine() 
        {
            CurrentState = State.OnField;
            StateChanged = new UnityEvent<State>();
        }

        public void UpdateState(State state)
        {
            CurrentState = state;
            StateChanged.Invoke(CurrentState);
        }
    }
}
