using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Language;

namespace BattleSystem.UI
{
    public class BattleStartTag : MonoBehaviour
    {
        private UnityEvent _finishedHidingEvent;
        public UnityEvent FinishedHidingEvent
        {
            get
            {
                if (_finishedHidingEvent == null)
                    _finishedHidingEvent = new UnityEvent();

                return _finishedHidingEvent;
            }
        }

        private Animator _Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public void Show()
        {
            if(_Animator)
                _Animator.Play("ShowBattleStartTag");
        }

        public void Hide()
        {
            if(_Animator)
                _Animator.Play("HideBattleStartTag");
        }

        public void FinishedHiding()
        {
            FinishedHidingEvent.Invoke();
        }

        public void FinishedShowing()
        {
            StartCoroutine(DoHide());
        }

        private IEnumerator DoHide()
        {
            yield return new WaitForSeconds(2.0f);
            Hide();
        }

        public void UpdateForVictory()
        {
            GetComponentInChildren<LocalizedText>().UpdateKey("battleVictory");
            Show();
        }

        public void UpdateForDefeat()
        {
            GetComponentInChildren<LocalizedText>().UpdateKey("battleDefeat");
            Show();
        }
    }
}
