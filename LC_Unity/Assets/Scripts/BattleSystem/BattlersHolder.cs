using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Model;

namespace BattleSystem
{
    public class BattlersHolder : MonoBehaviour
    {
        [SerializeField]
        private List<BattlerBehaviour> _battlers;

        public BattlerBehaviour InstantiateBattler(Battler battler)
        {
            BattlerBehaviour instBattler = Instantiate(_battlers.FirstOrDefault(b => b.BattlerId == battler.Id));
            instBattler.Feed(battler);

            return instBattler;
        }
    }
}
