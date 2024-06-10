using FlowControl;

namespace Engine.FlowControl
{
    public class VariableCondition : ConditionalBranch
    {
        public enum Type { Equal, GreaterThan, SmallerThan, Different, EqualOrGreaterThan, EqualOrSmallerThan }

        public Type Condition { get; set; }

        public override void Run()
        {
            bool result = ConditionEvaluator.Instance.EvaluateVariableCondition(this);

            DefineSequences(result);
        }
    }
}
