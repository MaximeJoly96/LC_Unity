using UnityEngine;
using System.Collections;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        private bool _moveNext;

        [SerializeField]
        private TextAsset _test;

        private void Awake()
        {
            RunEvents();
        }

        public void RunEvents()
        {
            var sequence = EventsSequenceParser.ParseEventsSequence(_test);

            RunEvents(sequence);
        }

        public void RunEvents(EventsSequence sequence)
        {
            StartCoroutine(RunSequence(sequence));
        }

        private void MoveNext()
        {
            _moveNext = true;
        }

        private IEnumerator RunSequence(EventsSequence sequence)
        {
            for (int i = 0; i < sequence.Events.Count; i++)
            {
                _moveNext = false;
                sequence.Events[i].Finished.AddListener(MoveNext);

                sequence.Events[i].Run();

                yield return new WaitUntil(() => _moveNext);
            }
        }
    }
}

