using Logging;
using UnityEngine.Events;

namespace Core
{
    public class GlobalStateMachine
    {
        public enum State
        {
            TitleScreen,
            TitleScreenOptions,

            OnField,
            InMenu,
            Interacting,
            ClosingMenu,
            OpeningMenu,
            SelectingCharacterPreview,
            InMenuItemsTab,
            BrowsingInventory,
            InMenuEquipmentTab,
            ChangingEquipment,
            InMenuAbilitiesTab,
            InMenuStatusTab,
            InMenuQuestsTab,
            BrowsingQuests,
            InMenuSystemTab,
            TransitionCharacterTarget,
            InCharacterTargetMenu,
            OpeningMessageBox,
            InMessageBox,
            ClosingMessageBox,

            OpeningSaves,
            BrowsingSaves,
            ClosingSaves,

            OpeningShop,
            InShopOptions,
            InShopBuyList,
            InShopSellList,
            BuyingItems,
            SellingItems,
            ClosingShop,
            PromptedToSkipScene,

            LoadingBattle
        }

        private State _rememberedState;
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
        public int CurrentMapId { get; set; } = -1;

        private GlobalStateMachine() 
        {
            CurrentState = State.OnField;
            StateChanged = new UnityEvent<State>();
        }

        public void UpdateState(State state)
        {
            // There is a weird situation caused by Unity's script execution order
            // Basically, when we load a save through the menu (by saving or loading), the state will
            // be updated to OnField because we load the map, but then the save window finishes closing
            // (it has DontDestroyOnLoad) so its closing event is fired right after.
            // So we are cancelling this specific transition to prevent getting stuck on the map.
            if (CurrentState == State.OnField && state == State.InMenuSystemTab)
                return;

            CurrentState = state;
            StateChanged.Invoke(CurrentState);
        }

        public void RememberState()
        {
            _rememberedState = CurrentState;
        }

        public void LoadRememberedState()
        {
            UpdateState(_rememberedState);
        }
    }
}
