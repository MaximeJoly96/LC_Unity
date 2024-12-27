using UnityEngine;
using TMPro;
using Actors;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Menus
{
    public class TargetableCharacter : MonoBehaviour
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
        private Image[] _effects;

        public Character Character { get; private set; }

        public void Feed(Character character)
        {
            ClearEffects();
            Character = character;

            _name.text = character.Name;
            _health.text = character.Stats.CurrentHealth + "/" + character.Stats.MaxHealth + " HP";
            _mana.text = character.Stats.CurrentMana + "/" + character.Stats.MaxMana + " MP";
            _essence.text = character.Stats.CurrentEssence + "/" + character.Stats.MaxEssence + " EP";

            UpdateEffects(character.ActiveEffects);
        }

        private void ClearEffects()
        {
            for(int i = 0; i < _effects.Length; i++)
            {
                _effects[i].sprite = null;
                _effects[i].color = Color.clear;
            }
        }

        private void UpdateEffects(List<ActiveEffect> effects)
        {
            for(int i = 0; i < effects.Count; i++)
            {

            }
        }

        public void ShowCursor(bool show)
        {
            GetComponent<Image>().enabled = show;
        }

        public void Disable(bool disable)
        {
            Color toUse = disable ? Color.grey : Color.white;

            _name.color = toUse;
            _health.color = toUse;
            _mana.color = toUse;
            _essence.color = toUse;
        }
    }
}
