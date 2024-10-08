﻿using UnityEngine;
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

        public void Feed(Character character)
        {
            ClearEffects();

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
    }
}
