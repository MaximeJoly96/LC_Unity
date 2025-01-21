using UnityEngine.Events;
using UnityEngine;
using Engine.Events;
using System.Collections;
using TMPro;
using System.Text;

namespace Dialogs
{
    public class UiBox : UiBox<IRunnable> { }

    public class UiBox<T> : MonoBehaviour where T : IRunnable
    {
        protected T _element;
        protected AudioSource _audio;

        public Animator Animator { get { return GetComponent<Animator>(); } }
        public UnityEvent HasClosed { get; set; }
        public UnityEvent HasFinishedOpening { get; set; }
        
        protected virtual string OpenAnimatioName { get; }
        protected virtual string CloseAnimationName { get; }

        public virtual void Feed(T element)
        {
            _element = element;
            _audio = GetComponent<AudioSource>();
        }

        public virtual void Open()
        {
            HasClosed = new UnityEvent();
            HasFinishedOpening = new UnityEvent();
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
            _element.IsFinished = true;
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
                if (_audio)
                    _audio.Play();
                yield return wait;
            }
        }
    }
}