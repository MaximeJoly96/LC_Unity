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
            BattlerBehaviour toInst = _battlers.FirstOrDefault(b => b.BattlerId == battler.Character.Id);
            BattlerBehaviour instBattler = Instantiate(toInst, battler.InitialPosition, Quaternion.identity);
            instBattler.Feed(battler);

            return instBattler;
        }
    }
}
