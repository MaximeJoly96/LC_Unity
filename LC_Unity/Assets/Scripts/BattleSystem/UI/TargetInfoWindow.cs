using UnityEngine;
using TMPro;
using Actors;

namespace BattleSystem.UI
{
    public class TargetInfoWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _health;
        [SerializeField]
        private TMP_Text _mana;
        [SerializeField]
        private TMP_Text _essence;
        [SerializeField]
        private Transform _activeEffects;

        public void Feed(Character character)
        {
            _name.text = character.Name;
            _health.text = character.CurrentHealth + "/" + character.MaxHealth + " HP";
            _mana.text = character.CurrentMana + "/" + character.MaxMana + " MP";
            _essence.text = character.CurrentEssence + "/" + character.MaxEssence + " EP";
        }
    }
}
