using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Engine.SceneControl;
using System.Collections.Generic;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        private static List<IRunnable> _postBattleEvents;

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

        public static List<IRunnable> PostBattleEvents
        {
            get { return _postBattleEvents; }
        }

        public void RunEvents(EventsSequence sequence)
        {
            StartCoroutine(RunSequence(sequence));
        }

        private IEnumerator RunSequence(EventsSequence sequence)
        {
            for (int i = 0; i < sequence.Events.Count; i++)
            {
                if (sequence.Events[i] is BattleProcessing && i + 1 < sequence.Events.Count)
                {
                    _postBattleEvents = new List<IRunnable>();
                    for(int j = i + 1; j < sequence.Events.Count; j++)
                    {
                        _postBattleEvents.Add(sequence.Events[j]);
                    }
                }

                sequence.Events[i].Run();

                yield return new WaitUntil(() => sequence.Events[i].IsFinished);

                sequence.Events[i].IsFinished = false;
            }

            sequence.Finished.Invoke();
            Finished.Invoke();
        }
    }
}

