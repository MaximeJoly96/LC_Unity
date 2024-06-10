using FlowControl;

namespace Engine.FlowControl
{
    public class TimerCondition : ConditionalBranch
    {
        public enum Type { Before, After }

        public Type Condition { get; set; }

        public override void Run()
        {
            bool result = ConditionEvaluator.Instance.EvaluateTimerCondition(this);

            DefineSequences(result);
        }
    }
}
