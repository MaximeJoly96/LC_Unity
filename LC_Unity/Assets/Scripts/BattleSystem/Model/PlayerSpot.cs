namespace BattleSystem.Model
{
    public class PlayerSpot
    {
        public int Id { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }

        public PlayerSpot(int id, float x, float y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
