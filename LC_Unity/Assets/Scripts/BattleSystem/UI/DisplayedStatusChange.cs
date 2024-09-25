using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Actors;
using Language;
using Utils;

namespace BattleSystem.UI
{
    public class DisplayedStatusChange : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _statusLabel;
        [SerializeField]
        private Image _statusIcon;

        public void Feed(EffectType effect, bool add)
        {
            _statusLabel.text = (add ? "+" : "-") + Localizer.Instance.GetString(LanguageUtility.GetEffectTypeLanguageKey(effect));
            _statusIcon.sprite = FindObjectOfType<EffectTypesWrapper>().GetSpriteFromEffectType(effect);
        }

        public void Show()
        {
            GetComponent<Animator>().Play("Show");
        }

        public void FinishedShowing()
        {
            Destroy(gameObject);
        }
    }
}
