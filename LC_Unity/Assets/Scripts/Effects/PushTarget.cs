using BattleSystem;
using BattleSystem.Behaviours;
using UnityEngine;
using Utils;

namespace Effects
{
    public class PushTarget : IEffect
    {
        public int Distance { get; set; }

        public string GetDescription()
        {
            return "";
        }

        public void Apply(BattlerBehaviour source, BattlerBehaviour target)
        {
            BattlerMoverBehaviour mover = target.gameObject.AddComponent<BattlerMoverBehaviour>();
            Vector2 direction = (target.transform.position - source.transform.position).normalized;
            float distance = MeasuresConverter.RangeToWorldUnits(Distance);

            mover.Feed(direction, distance);
        }
    }
}
