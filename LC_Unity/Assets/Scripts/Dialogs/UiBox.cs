using UnityEngine.Events;
using UnityEngine;
using Engine.Events;
using System.Collections;
using TMPro;
using System.Text;

namespace Dialogs
{
    public abstract class UiBox<T> : MonoBehaviour where T : IRunnable
    {
        protected T _element;

        public Animator Animator { get { return GetComponent<Animator>(); } }
        public UnityEvent HasClosed { get; set; }
        
        protected abstract string OpenAnimatioName { get; }
        protected abstract string CloseAnimationName { get; }

        public virtual void Feed(T element)
        {
            _element = element;
        }

        public virtual void Open()
        {
            HasClosed = new UnityEvent();
            Animator.Play(OpenAnimatioName);
        }

        public virtual void Close()
        {
            Animator.Play(CloseAnimationName);
        }

        public virtual void FinishedClosing()
        {
            _element.Finished.Invoke();
            HasClosed.Invoke();
        }

        public virtual void FinishedOpening() { }

        protected IEnumerator AnimateText(TMP_Text text, string message)
        {
            StringBuilder builder = new StringBuilder();
            WaitForEndOfFrame wait = new WaitForEndOfFrame();

            for (int i = 0; i < message.Length; i++)
            {
                builder = builder.Append(message[i]);
                text.text = builder.ToString();
                yield return wait;
            }
        }
    }
}