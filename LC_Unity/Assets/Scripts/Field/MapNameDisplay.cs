using UnityEngine;
using TMPro;
using Language;
using Core;

namespace Field
{
    public class MapNameDisplay : MonoBehaviour
    {
        private const float DELAY_BEFORE_HIDE = 5.0f;

        [SerializeField]
        private TMP_Text _mapName;

        private bool _shown;
        private float _displayTimer;
        private Animator _animator;

        public bool DisplayEnabled { get; set; } = true;
        public Animator Animator
        {
            get
            {
                if(!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }
        private TMP_Text MapName
        {
            get
            {
                if(!_mapName)
                {
                    _mapName = GetComponent<TMP_Text>();

                    if(!_mapName)
                        _mapName = GetComponentInChildren<TMP_Text>();
                }

                return _mapName;
            }
        }

        public void Show()
        {
            if(DisplayEnabled)
            {
                UpdateMapName();

                if(Animator)
                    Animator.Play("ShowMapName");
            }
        }

        public void FinishedShowing()
        {
            _shown = true;
            _displayTimer = 0.0f;
        }

        private void UpdateMapName()
        {
            MapName.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[GlobalStateMachine.Instance.CurrentMapId]);
        }

        private void Update()
        {
            if(_shown)
            {
                _displayTimer += Time.deltaTime;

                if(_displayTimer > DELAY_BEFORE_HIDE)
                {
                    _shown = false;
                    _displayTimer = 0.0f;

                    if(Animator)
                        Animator.Play("HideMapName");
                }
            }
        }
    }
}
