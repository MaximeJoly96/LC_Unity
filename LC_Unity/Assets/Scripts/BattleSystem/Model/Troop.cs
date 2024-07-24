using System.Collections.Generic;

namespace BattleSystem.Model
{
    public class Troop
    {
        public int Id { get; set; }
        public List<int> Members { get; set; }

        public Troop(int id, List<int> members)
        {
            Id = id;
            Members = members;
        }
    }
}
