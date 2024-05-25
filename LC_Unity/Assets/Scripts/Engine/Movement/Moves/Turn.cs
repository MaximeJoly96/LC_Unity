namespace Engine.Movement.Moves
{
    public class Turn : Move
    {
        public enum PossibleDirection { Left, Right, Bottom, Top }

        public PossibleDirection Direction { get; set; }

        public override void Run()
        {
            
        }
    }
}
