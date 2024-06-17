using Engine.Events;
using UnityEngine;
using Logging;

namespace Field
{
    public enum AgentTrigger
    {
        Manual,
        Foreground,
        Background,
        Contact
    }

    public class RunnableAgent : Agent
    {
        [SerializeField]
        private TextAsset _sequenceFile;
        [SerializeField]
        private AgentTrigger _trigger;

        private EventsSequence _sequence;

        public EventsRunner Runner
        {
            get
            {
                EventsRunner runner = GetComponent<EventsRunner>();

                if (!runner)
                    LogsHandler.Instance.LogError("There is no EventsRunner attached to a RunnableAgent but you are trying to access it.");

                return runner;
            }
        }

        public void SetSequence(EventsSequence sequence)
        {
            _sequence = sequence;
        }

        public void RunSequence()
        {
            if (_sequence == null)
            {
                if (_sequenceFile == null)
                {
                    LogsHandler.Instance.LogError("You're trying to run a sequence on an Agent but the sequence is null and it has no TextAsset.");
                    return;
                }

                SetSequence(EventsSequenceParser.ParseEventsSequence(_sequenceFile));
            }

            Runner.RunEvents(_sequence); 
        }
    }
}
