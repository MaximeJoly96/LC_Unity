using Actors;
using UnityEngine;

namespace BattleSystem.Model
{
    public class Battler
    {
        public Character Character { get; set; }
        public Vector3 InitialPosition { get; set; }

        public Battler(Character character)
        {
            Character = character;
        }
    }
}
