using UnityEngine;
using Actors;
using TMPro;
using UnityEngine.UI;

namespace Menus
{
    public class CharacterPreview : MonoBehaviour
    {
        private Character _character;
        private Animator _animator;

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
        public Transform Faceset { get { return _faceset; } }
        public TMP_Text Name { get { return _name; } }
        public StatGauge HpGauge { get { return _hpGauge; } }
        public StatGauge ManaGauge { get { return _manaGauge; } }
        public StatGauge EssenceGauge { get { return _essenceGauge; } }
        public XpGauge XpGauge { get { return _xpGauge; } }

        private Animator Animator
        {
            get
            {
                if(!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

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
            if(Animator)
                Animator.Play("Hovered");
        }

        public void Unselect()
        {
            if(Animator)
                Animator.Play("Idle");
        }

        // For unit testing purposes
        public void Init()
        {
            if(!_faceset)
                _faceset = GetComponentsInChildren<Image>()[0].transform;

            if(!_name)
                _name = GetComponentsInChildren<TextMeshProUGUI>()[0];

            if(!_hpGauge)
                _hpGauge = GetComponentsInChildren<StatGauge>()[0];

            if (!_manaGauge)
                _manaGauge = GetComponentsInChildren<StatGauge>()[1];

            if (!_essenceGauge)
                _essenceGauge = GetComponentsInChildren<StatGauge>()[2];

            if (!_xpGauge)
                _xpGauge = GetComponentsInChildren<XpGauge>()[0];
        }
    }
}
