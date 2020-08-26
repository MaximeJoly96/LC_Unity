using System.Collections.Generic;

namespace RPG_Maker_VX_Ace_Import.Database.Battlers
{
    public class Ability
    {
        private int _id;
        private string _name;
        private List<IAbilityBehaviour> _behaviours;

        public int ID { get { return _id; } }
        public string Name { get { return _name; } }
        public List<IAbilityBehaviour> Behaviours { get { return _behaviours; } }

        public Ability(int id, string name)
        {
            _id = id;
            _name = name;
            _behaviours = new List<IAbilityBehaviour>();
        }

        public void AddNewBehaviour(IAbilityBehaviour behaviour)
        {
            _behaviours.Add(behaviour);
        }
    }
}

