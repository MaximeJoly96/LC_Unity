using Engine.Events;
using UnityEngine.Events;

namespace Engine.Movement
{
    public class TransferObject : IRunnable
    {
        public enum PossibleDirection { Left, Right, Top, Bottom, Retain }
        public enum FadeType { Normal, White, None }

        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public PossibleDirection Direction { get; set; }
        public FadeType Fade { get; set; }
        public UnityEvent Finished { get; set; }
        public bool IsFinished { get; set; }

        public TransferObject()
        {
            Finished = new UnityEvent();
        }

        public void Run()
        {

        }
    }
}
