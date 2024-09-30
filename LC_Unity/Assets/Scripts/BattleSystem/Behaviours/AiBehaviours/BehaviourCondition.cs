namespace BattleSystem.Behaviours.AiBehaviours
{
    public abstract class BehaviourCondition
    {
        public BehaviourAction ActionWhenTrue { get; private set; }
        public BehaviourAction ActionWhenFalse { get; private set; }

        protected BehaviourCondition() : this(new BehaviourAction(), new BehaviourAction()) { }

        protected BehaviourCondition(BehaviourAction actionWhenTrue, BehaviourAction actionWhenFalse)
        {
            SetActions(actionWhenTrue, actionWhenFalse);
        }

        public void SetActions(BehaviourAction whenTrue, BehaviourAction whenFalse)
        {
            ActionWhenTrue = whenTrue;
            ActionWhenFalse = whenFalse;
        }
    }
}
