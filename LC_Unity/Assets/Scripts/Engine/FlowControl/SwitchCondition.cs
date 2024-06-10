using FlowControl;

namespace Engine.FlowControl
{
    public class SwitchCondition : ConditionalBranch
    {
        public enum Type { Equal, NotEqual }
        public Type Condition { get; set; }

        public override void Run()
        {
            bool result = ConditionEvaluator.Instance.EvaluateSwitchCondition(this);

            DefineSequences(result);
        }
    }
}
