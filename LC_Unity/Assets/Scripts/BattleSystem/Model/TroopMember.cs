namespace BattleSystem.Model
{
    public class TroopMember
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Id { get; private set; }

        public TroopMember(int id, float x, float y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
