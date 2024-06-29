using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Actors;

namespace BattleSystem.UI
{
    public class PlayerStatsUiPreview : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _health;
        [SerializeField]
        private TMP_Text _mana;
        [SerializeField]
        private TMP_Text _essence;
        [SerializeField]
        private Image[] _status;

        public void Feed(Character character)
        {
            _health.text = character.BaseHealth.CurrentValue + "/" + character.BaseHealth.MaxValue + " HP";
            _mana.text = character.BaseMana.CurrentValue + "/" + character.BaseMana.MaxValue + " MP";
            _essence.text = character.BaseEssence.CurrentValue + "/" + character.BaseEssence.MaxValue + " EP";

            for(int i = 0; i < _status.Length && i < character.ActiveEffects.Count; i++)
            {
                _status[i].sprite = null;
            }
        }
    }
}
