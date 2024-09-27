using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    public class AbilitiesManager
    {
        private static AbilitiesManager _instance;

        public static AbilitiesManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AbilitiesManager();

                return _instance;
            }
        }

        public List<Ability> Abilities { get; private set; }

        private AbilitiesManager() { }

        public void Init(TextAsset data)
        {
            Abilities = AbilityParser.ParseAllAbilities(data);
        }

        public Ability GetAbility(int id)
        {
            return Abilities != null ? Abilities.FirstOrDefault(a => a.Id == id) : null;
        }
    }
}
