using FlowControl;

namespace Engine.FlowControl
{
    public class SwitchCondition : ConditionalBranch
    {
        public enum Type { Equal, NotEqual }
        public Type Condition { get; set; }

        public override void Run()
        {
            ConditionEvaluator.Instance.EvaluateSwitchCondition(this);
            Finished.Invoke();
        }
    }
}
