using UnityEngine;
using TMPro;
using Language;
using System.Collections;
using System.Text;
using UnityEngine.Events;

namespace BattleSystem.UI
{
    public class BattleInitInstructionsWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _instructionsText;

        private UnityEvent _instructionsWindowClosed;
        public UnityEvent InstructionsWindowClosed
        {
            get
            {
                if (_instructionsWindowClosed == null)
                    _instructionsWindowClosed = new UnityEvent();

                return _instructionsWindowClosed;
            }
        }

        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void ShowWindow()
        {
            Animator.Play("InstructionsWindowOpen");
        }

        public void HideWindow()
        {
            _instructionsText.text = "";
            Animator.Play("InstructionsWindowClose");
        }

        public void FinishedShowing()
        {
            UpdateText("battleInitInstructionsSelectionPhase");
        }

        public void FinishedClosing()
        {
            InstructionsWindowClosed.Invoke();
            gameObject.SetActive(false);
        }

        public void UpdateText(string key)
        {
            StartCoroutine(AnimateText(Localizer.Instance.GetString(key)));
        }

        private IEnumerator AnimateText(string text)
        {
            _instructionsText.text = "";

            StringBuilder builder = new StringBuilder();
            WaitForEndOfFrame wait = new WaitForEndOfFrame();

            for (int i = 0; i < text.Length; i++)
            {
                builder = builder.Append(text[i]);
                _instructionsText.text = builder.ToString();
                yield return wait;
            }
        }
    }
}
