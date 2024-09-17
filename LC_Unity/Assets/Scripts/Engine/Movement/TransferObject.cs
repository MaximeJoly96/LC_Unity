using Engine.Events;
using UnityEngine.Events;
using UnityEngine;
using Movement;

namespace Engine.Movement
{
    public class TransferObject : IRunnable
    {
        public enum PossibleDirection { Left, Right, Top, Bottom, Retain }

        public string Target { get; set; }
        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public PossibleDirection Direction { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public TransferObject()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {
            Object.FindObjectOfType<TransferObjectManager>().MoveObject(this);
        }
    }
}
