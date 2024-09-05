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

        public bool DisplayEnabled { get; set; } = true;

        public void Show()
        {
            if(DisplayEnabled)
            {
                UpdateMapName();
                GetComponent<Animator>().Play("ShowMapName");
            }
        }

        public void FinishedShowing()
        {
            _shown = true;
            _displayTimer = 0.0f;
        }

        private void UpdateMapName()
        {
            _mapName.text = Localizer.Instance.GetString(FieldNames.MAP_NAMES[GlobalStateMachine.Instance.CurrentMapId]);
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
                    GetComponent<Animator>().Play("HideMapName");
                }
            }
        }
    }
}
