using UnityEngine;
using Actors;
using TMPro;

namespace Menus
{
    public class CharacterPreview : MonoBehaviour
    {
        private Character _character;

        [SerializeField]
        private Transform _faceset;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private StatGauge _hpGauge;
        [SerializeField]
        private StatGauge _manaGauge;
        [SerializeField]
        private StatGauge _essenceGauge;
        [SerializeField]
        private XpGauge _xpGauge;

        public Character Character { get { return _character; } }

        public void Feed(Character character)
        {
            _character = character;

            _name.text = character.Name;
            _hpGauge.SetGauge(character.Stats.CurrentHealth, character.Stats.MaxHealth);
            _manaGauge.SetGauge(character.Stats.CurrentMana, character.Stats.MaxMana);
            _essenceGauge.SetGauge(character.Stats.CurrentEssence, character.Stats.MaxEssence);

            _xpGauge.SetLevel(character.GetXpForCurrentLevel(), character.GetXpRequiredForLevel(character.Stats.Level), character.Stats.Level);
        }

        public void Hover()
        {
            GetComponent<Animator>().Play("Hovered");
        }

        public void Unselect()
        {
            GetComponent<Animator>().Play("Idle");
        }
    }
}
