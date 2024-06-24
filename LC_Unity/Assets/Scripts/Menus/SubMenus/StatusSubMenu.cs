using Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Menus.SubMenus.Status;

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
                _health.SetGauge(_fedCharacter.BaseHealth.CurrentValue, _fedCharacter.BaseHealth.MaxValue);
                _mana.SetGauge(_fedCharacter.BaseMana.CurrentValue, _fedCharacter.BaseMana.MaxValue);
                _essence.SetGauge(_fedCharacter.BaseEssence.CurrentValue, _fedCharacter.BaseEssence.MaxValue);

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
    }
}
