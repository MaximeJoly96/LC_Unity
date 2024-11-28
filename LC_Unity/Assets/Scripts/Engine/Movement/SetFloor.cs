using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Field;

namespace Engine.Movement
{
    public class SetFloor : IRunnable
    {
        public int FieldId { get; set; }
        public int FloorId { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public SetFloor()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<FieldBuilder>().SetFloor(this);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
