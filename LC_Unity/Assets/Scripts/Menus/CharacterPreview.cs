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

        public void Feed(Character character)
        {
            _character = character;

            _name.text = character.Name;
            _hpGauge.SetGauge(character.BaseHealth.CurrentValue, character.BaseHealth.MaxValue);
            _manaGauge.SetGauge(character.BaseMana.CurrentValue, character.BaseMana.MaxValue);
            _essenceGauge.SetGauge(character.BaseEssence.CurrentValue, character.BaseEssence.MaxValue);

            _xpGauge.SetLevel(character.GetXpForCurrentLevel(), character.GetXpRequiredForLevel(character.Level), character.Level);
        }
    }
}
