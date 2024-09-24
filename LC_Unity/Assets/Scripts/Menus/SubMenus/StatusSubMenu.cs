using Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Menus.SubMenus.Status;
using Inputs;
using Utils;

namespace Menus.SubMenus
{
    public class StatusSubMenu : SubMenu
    {
        [SerializeField]
        private TMP_Text _characterName;
        [SerializeField]
        private StatGauge _health;
        [SerializeField]
        private StatGauge _mana;
        [SerializeField]
        private StatGauge _essence;
        [SerializeField]
        private Image _xpGauge;
        [SerializeField]
        private TMP_Text _level;
        [SerializeField]
        private TMP_Text _xpValue;
        [SerializeField]
        private StatusSubPanel _statsPanel;
        [SerializeField]
        private StatusSubPanel _affinitiesPanel;
        [SerializeField]
        private StatusSubPanel _equipmentPanel;
        [SerializeField]
        private StatusSubPanel _effectsPanel;
        [SerializeField]
        private StatusSubPanel _essenceAffinityPanel;

        public override void Open()
        {
            Init();

            StartCoroutine(DoOpen());
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.InMenuStatusTab);
        }

        public override void Close()
        {
            StartCoroutine(DoClose());
        }

        protected override void FinishedClosing()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.SelectingCharacterPreview);
        }

        protected void Init()
        {
            if(_fedCharacter != null)
            {
                _characterName.text = _fedCharacter.Name;
                _health.SetGauge(_fedCharacter.CurrentHealth, _fedCharacter.MaxHealth);
                _mana.SetGauge(_fedCharacter.CurrentMana, _fedCharacter.MaxMana);
                _essence.SetGauge(_fedCharacter.CurrentEssence, _fedCharacter.MaxEssence);

                _level.text = (_fedCharacter.Level + 1).ToString();

                float currentLvlXp = _fedCharacter.GetXpForCurrentLevel();
                float requiredXp = _fedCharacter.GetXpRequiredForLevel(_fedCharacter.Level);

                _xpGauge.fillAmount = currentLvlXp / requiredXp;
                _xpValue.text = currentLvlXp + " / " + requiredXp;

                _statsPanel.Feed(_fedCharacter);
                _affinitiesPanel.Feed(_fedCharacter);
                _equipmentPanel.Feed(_fedCharacter);
                _effectsPanel.Feed(_fedCharacter);
                _essenceAffinityPanel.Feed(_fedCharacter);
            }
        }

        protected override void HandleInputs(InputAction input)
        {
            if (!_busy && GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InMenuStatusTab)
            {
                switch (input)
                {
                    case InputAction.Cancel:
                        CommonSounds.ActionCancelled();
                        Close();
                        break;
                    case InputAction.Select:
                        CommonSounds.CursorMoved();
                        (_statsPanel as StatsPanel).ChangePage();
                        break;
                }
            }
        }
    }
}
