﻿using Engine.Events;
using UnityEngine;
using Logging;
using UnityEngine.Events;
using Field.Routines;

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
        private UnityEvent _finishedSequence;

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

        public UnityEvent FinishedSequence
        {
            get
            {
                if (_finishedSequence == null)
                    _finishedSequence = new UnityEvent();

                return _finishedSequence;
            }
        }

        public AgentTrigger Trigger { get { return _trigger; } }

        public void SetSequence(EventsSequence sequence)
        {
            _sequence = sequence;
        }

        public virtual void RunSequence()
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

            InterruptRoutine();
            Runner.Finished.RemoveAllListeners();

            Runner.Finished.AddListener(() => 
            {
                FinishedSequence.Invoke();
                ResumeRoutine();
            });
            Runner.RunEvents(_sequence); 
        }

        protected virtual void OnEnable()
        {
            if (_trigger == AgentTrigger.Foreground)
                RunSequence();
        }

        protected virtual void InterruptRoutine()
        {
            RoutineRunner routineRunner = GetComponent<RoutineRunner>();

            if (routineRunner)
                routineRunner.Interrupt();
        }

        protected virtual void ResumeRoutine()
        {
            RoutineRunner routineRunner = GetComponent<RoutineRunner>();

            if (routineRunner)
                routineRunner.Resume();
        }
    }
}
