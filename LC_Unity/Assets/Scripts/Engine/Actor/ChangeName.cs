﻿using Engine.Events;
using UnityEngine.Events;
using Actors;
using Party;

namespace Engine.Actor
{
    public class ChangeName : IRunnable
    {
        public int CharacterId { get; set; }
        public string Value { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangeName()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            PartyManager.Instance.ChangeCharacterName(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
