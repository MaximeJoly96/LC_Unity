using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Field;
using System.Collections.Generic;

namespace Engine.SystemSettings
{
    public class AllowCutsceneSkip : IRunnable
    {
        public bool Allow { get; set; }
        public EventsSequence ActionsWhenSkipping { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public AllowCutsceneSkip()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<CutsceneSkipper>().EnableSceneSkipping(this);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
