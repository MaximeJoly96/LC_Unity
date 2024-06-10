﻿using Engine.Events;
using UnityEngine.Events;

namespace Engine.ScreenEffects
{
    public class FadeScreen : IRunnable
    {
        public bool FadeIn { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public FadeScreen()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
