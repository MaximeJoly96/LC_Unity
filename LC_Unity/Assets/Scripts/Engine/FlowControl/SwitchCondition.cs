using FlowControl;
using UnityEngine;
using Engine.Events;

namespace Engine.FlowControl
{
    public class SwitchCondition : ConditionalBranch
    {
        public enum Type { Equal, NotEqual }
        public Type Condition { get; set; }

        public override void Run()
        {
            EventsRunner runner = Object.FindObjectOfType<EventsRunner>();
            bool result = ConditionEvaluator.Instance.EvaluateSwitchCondition(this);

            if (result)
            {
                SequenceWhenTrue.Finished.AddListener(Conclude);
                runner.RunEvents(SequenceWhenTrue);
            }
            else
            {
                SequenceWhenFalse.Finished.AddListener(Conclude);
                runner.RunEvents(SequenceWhenFalse);
            }
        }

        private void Conclude()
        {
            Finished.Invoke();
            IsFinished = true;
        }
    }
}
