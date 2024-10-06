using UnityEngine;
using TMPro;
using Actors;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

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
        [SerializeField]
        private Transform _targetActiveEffectPrefab;

        public void Feed(Character character)
        {
            _name.text = character.Name;
            _health.text = character.Stats.CurrentHealth + "/" + character.Stats.MaxHealth + " HP";
            _mana.text = character.Stats.CurrentMana + "/" + character.Stats.MaxMana + " MP";
            _essence.text = character.Stats.CurrentEssence + "/" + character.Stats.MaxEssence + " EP";

            DisplayActiveEffects(character.ActiveEffects);
        }

        private void DisplayActiveEffects(List<ActiveEffect> effects)
        {
            ClearCurrentEffects();

            for(int i = 0; i < 4 && i < effects.Count; i++)
            {
                Transform effect = Instantiate(_targetActiveEffectPrefab, _activeEffects);
                effect.GetComponent<Image>().sprite = FindObjectOfType<EffectTypesWrapper>().GetSpriteFromEffectType(effects[i].Effect);
            }
        }

        private void ClearCurrentEffects()
        {
            foreach (Transform child in _activeEffects)
                Destroy(child.gameObject);
        }
    }
}
