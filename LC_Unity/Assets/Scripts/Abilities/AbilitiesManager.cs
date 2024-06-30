using System.Collections.Generic;
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
    }
}
