using Actors;

namespace BattleSystem.Model
{
    public class Battler
    {
        public Character Character { get; set; }

        public Battler(Character character)
        {
            Character = character;
        }
    }
}
