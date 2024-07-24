using UnityEngine;
using TMPro;
using Language;
using System.Collections;
using System.Text;

namespace BattleSystem.UI
{
    public class BattleInitInstructionsWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _instructionsText;

        public Animator Animator { get { return GetComponent<Animator>(); } }

        public void ShowWindow()
        {
            Animator.Play("InstructionsWindowOpen");
        }

        public void HideWindow()
        {
            Animator.Play("InstructionsWindowClose");
        }

        public void FinishedShowing()
        {
            UpdateText("battleInitInstructionsSelectionPhase");
        }

        public void FinishedClosing()
        {

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
