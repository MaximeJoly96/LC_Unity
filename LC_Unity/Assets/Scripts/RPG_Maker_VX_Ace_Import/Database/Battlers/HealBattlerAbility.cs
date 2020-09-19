using System.Linq;
using RPG_Maker_VX_Ace_Import.Database.System;

namespace RPG_Maker_VX_Ace_Import.Database.Battlers
{
    public class HealBattlerAbility : IAbilityBehaviour
    {
        public ElementalTypes Element
        {
            get { return ElementalTypes.Heal; }
        }

        public void Cast(Battler target)
        {
            target.Stats.FirstOrDefault(s => s.Label == CharacterStats.Health);
        }
    }
}
