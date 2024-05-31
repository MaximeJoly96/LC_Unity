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
            EventsSequenceParser parser = new EventsSequenceParser();
            var sequence = parser.ParseEventsSequence(_test);

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

