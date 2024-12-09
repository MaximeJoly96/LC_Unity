using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Model;
using System.Collections;

namespace BattleSystem
{
    public class BattlersHolder : MonoBehaviour
    {
        [SerializeField]
        private List<BattlerBehaviour> _battlers;

        private void Awake()
        {
            if(_battlers == null)
                _battlers = new List<BattlerBehaviour>();
        }

        public BattlerBehaviour InstantiateBattler(Battler battler)
        {
            BattlerBehaviour toInst = _battlers.FirstOrDefault(b => b.BattlerId == battler.Character.Id);
            BattlerBehaviour instBattler = Instantiate(toInst, battler.InitialPosition, Quaternion.identity);
            instBattler.Feed(battler);

            return instBattler;
        }

        public void Feed(IEnumerable<BattlerBehaviour> battlers)
        {
            foreach(BattlerBehaviour b in battlers)
            {
                _battlers.Add(b);
            }
        }
    }
}
