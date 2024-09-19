using Core;
using Engine.Events;
using UnityEngine.Events;
using System;

namespace Engine.SystemSettings
{
    public class ChangeGameState : IRunnable
    {
        public string State { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeGameState()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            GlobalStateMachine.State state = (GlobalStateMachine.State)Enum.Parse(typeof(GlobalStateMachine.State), State);
            GlobalStateMachine.Instance.UpdateState(state);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
