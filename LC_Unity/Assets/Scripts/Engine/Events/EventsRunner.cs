using UnityEngine;
using System.Collections;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _test;

        public void RunEvents()
        {
            var sequence = EventsSequenceParser.ParseEventsSequence(_test);

            RunEvents(sequence);
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
            }

            sequence.Finished.Invoke();
        }
    }
}

