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
            _health.text = character.Stats.CurrentHealth + "/" + character.Stats.MaxHealth + " HP";
            _mana.text = character.Stats.CurrentMana + "/" + character.Stats.MaxMana + " MP";
            _essence.text = character.Stats.CurrentEssence + "/" + character.Stats.MaxEssence + " EP";

            for(int i = 0; i < _status.Length && i < character.ActiveEffects.Count; i++)
            {
                _status[i].sprite = null;
            }
        }
    }
}
