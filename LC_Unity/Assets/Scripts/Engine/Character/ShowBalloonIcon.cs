﻿using Engine.Events;

namespace Engine.Character
{
    public class ShowBalloonIcon : IRunnable
    {
        public string Target { get; set; }
        public int BalloonIconId { get; set; }
        public bool WaitForCompletion { get; set; }

        public void Run()
        {

        }
    }
}
