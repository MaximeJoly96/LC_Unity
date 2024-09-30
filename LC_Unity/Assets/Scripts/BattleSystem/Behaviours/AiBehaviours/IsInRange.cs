using System.Linq;
using UnityEngine;
using Utils;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class IsInRange : BehaviourCondition
    {
        private int _minTargets;
        private int _maxTargets;
        private int _range;

        public int Range
        {
            get
            {
                return Mathf.Max(0, _range);
            }
        }

        public int MinTargetCount
        {
            get
            {
                return _minTargets > -1 ? _minTargets : 0;
            }
        }

        public int MaxTargetCount
        {
            get
            {
                return _maxTargets > - 1 ? _maxTargets : 0;
            }
        }

        public IsInRange() : this(-1, -1, 0) { }

        public IsInRange(int minTargets, int maxTargets, int range)
        {
            SetValues(minTargets, maxTargets, range);
        }

        public void SetValues(int minTargetCount, int maxTargetCount, int range)
        {
            _minTargets = minTargetCount;
            _maxTargets = maxTargetCount;
            _range = range;
        }

        public bool Check(GameObject source)
        {
            BattlerBehaviour[] targets = Object.FindObjectsOfType<BattlerBehaviour>();

            int targetsInRange = targets.Count(t => t != source.GetComponent<BattlerBehaviour>() &&
                                                    Vector2.Distance(source.transform.position, t.transform.position) < MeasuresConverter.RangeToWorldUnits(Range));

            bool isInRange = true;

            if (MinTargetCount > 0)
                isInRange = targetsInRange >= MinTargetCount;

            if(MaxTargetCount > 0)
                isInRange = targetsInRange <= MaxTargetCount;

            return isInRange;
        }
    }
}
