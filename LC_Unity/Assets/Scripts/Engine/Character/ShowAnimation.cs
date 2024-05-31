﻿using Engine.Events;
using UnityEngine.Events;

namespace Engine.Character
{
    public class ShowAnimation : IRunnable
    {
        public string Target { get; set; }
        public int AnimationId { get; set; }
        public bool WaitForCompletion { get; set; }
        public UnityEvent Finished { get; set; }

        public ShowAnimation()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
