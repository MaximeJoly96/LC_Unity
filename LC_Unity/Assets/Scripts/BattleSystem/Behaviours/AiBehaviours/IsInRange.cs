using UnityEngine;

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
        
        public override void Run()
        {

        }
    }
}
