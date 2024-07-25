using UnityEngine;
using System.Collections;
using UnityEngine.Events;

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

        public void Show()
        {
            GetComponent<Animator>().Play("ShowBattleStartTag");
        }

        public void Hide()
        {
            GetComponent<Animator>().Play("HideBattleStartTag");
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
    }
}
