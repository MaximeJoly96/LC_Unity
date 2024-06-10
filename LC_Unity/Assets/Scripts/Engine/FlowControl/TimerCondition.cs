using FlowControl;

namespace Engine.FlowControl
{
    public class TimerCondition : ConditionalBranch
    {
        public enum Type { Greater, Smaller }

        public Type Condition { get; set; }

        public override void Run()
        {
            ConditionEvaluator.Instance.EvaluateTimerCondition(this);
            Finished.Invoke();
        }
    }
}
