using UnityEngine;
using BattleSystem.Model;

namespace BattleSystem
{
    public class BattlerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _battlerId;

        public int BattlerId { get { return _battlerId; } }

        public Battler BattlerData { get; private set; }

        public void Feed(Battler battler)
        {
            BattlerData = battler;
            _battlerId = BattlerData.Id;
        }
    }
}
