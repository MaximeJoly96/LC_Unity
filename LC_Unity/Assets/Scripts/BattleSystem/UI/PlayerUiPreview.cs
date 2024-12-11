using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Actors;
using Abilities;
using Language;

namespace BattleSystem.UI
{
    public class PlayerUiPreview : MonoBehaviour
    {
        [SerializeField]
        private Image _background;
        [SerializeField]
        private Image _playerIcon;
        [SerializeField]
        private TMP_Text _playerName;
        [SerializeField]
        private TMP_Text _currentAction;
        [SerializeField]
        private PlayerStatsUiPreview _playerStatsPreview;

        public string PlayerName { get { return _playerName.text; } }

        public void Feed(Character character)
        {
            _playerName.text = character.Name;
            _playerStatsPreview.Feed(character);
            _currentAction.text = Localizer.Instance.GetString("noAction");
        }

        public void UpdateCharacterAction(Ability ability)
        {
            _currentAction.text = Localizer.Instance.GetString(ability.Name);
        }
    }
}
