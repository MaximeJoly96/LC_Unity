using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        private UnityEvent _finished;

        public UnityEvent Finished
        {
            get
            {
                if (_finished == null)
                    _finished = new UnityEvent();

                return _finished;
            }
        }

        public void RunEvents(EventsSequence sequence)
        {
            StartCoroutine(RunSequence(sequence));
        }

        private IEnumerator RunSequence(EventsSequence sequence)
        {
            for (int i = 0; i < sequence.Events.Count; i++)
            {
                sequence.Events[i].Run();

                yield return new WaitUntil(() => sequence.Events[i].IsFinished);

                sequence.Events[i].IsFinished = false;
            }

            sequence.Finished.Invoke();
            Finished.Invoke();
        }
    }
}

