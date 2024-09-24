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
            _hpGauge.SetGauge(character.CurrentHealth, character.MaxHealth);
            _manaGauge.SetGauge(character.CurrentMana, character.MaxMana);
            _essenceGauge.SetGauge(character.CurrentEssence, character.MaxEssence);

            _xpGauge.SetLevel(character.GetXpForCurrentLevel(), character.GetXpRequiredForLevel(character.Level), character.Level);
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
