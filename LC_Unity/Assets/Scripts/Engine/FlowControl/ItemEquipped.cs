using Party;
using System.Collections.Generic;
using System.Linq;

namespace Engine.FlowControl
{
    public class ItemEquipped : InventoryCondition
    {
        public override void Run()
        {
            List<Actors.Character> party = PartyManager.Instance.GetParty();

            DefineSequences(party.Any(c => c.HasItemEquipped(ItemId)));
        }
    }
}
