﻿using Engine.Events;
using Field;
using UnityEngine.Events;

namespace Engine.Character
{
    public class ShowAgentAnimation : IRunnable
    {
        public bool IsFinished { get; set; }
        public UnityEvent Finished { get; set; }
        public string AnimationName { get; set; }
        public bool WaitForCompletion { get; set; }
        public string Target { get; set; }

        public ShowAgentAnimation()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            AgentsManager.Instance.ShowAgentAnimation(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
