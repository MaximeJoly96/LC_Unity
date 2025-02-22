﻿using Engine.Events;
using UnityEngine.Events;
using Party;

namespace Engine.Party
{
    public class ChangePartyMember : IRunnable
    {
        public enum ActionType { Add, Remove }

        public int Id { get; set; }
        public ActionType Action { get; set; }
        public bool Initialize { get; set; }
        public bool Notify { get; set; } = true;
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public ChangePartyMember()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            PartyManager.Instance.ChangePartyMember(this);
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
