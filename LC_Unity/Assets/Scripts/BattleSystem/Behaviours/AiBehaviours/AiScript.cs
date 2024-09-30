namespace BattleSystem.Behaviours.AiBehaviours
{
    public class AiScript
    {
        public BehaviourCondition MainCondition { get; private set; }
        public System.Random Rng { get; private set; }

        public AiScript()
        {
            Rng = new System.Random();
        }

        public void SetMainCondition(BehaviourCondition condition)
        {
            MainCondition = condition;
        }

        public int PickAction(BattlerBehaviour source)
        {
            if(MainCondition is DefaultCondition)
            {
                return MainCondition.ActionWhenTrue.Abilities[Rng.Next(MainCondition.ActionWhenTrue.Abilities.Count)];
            }
            else
            {
                return ParseConditionOtherThanDefault(MainCondition, source);
            }
        }

        private int ParseConditionOtherThanDefault(BehaviourCondition condition, BattlerBehaviour source)
        {
            if(condition is IsInRange)
            {
                IsInRange isInRange = condition as IsInRange;
                return isInRange.Check(source.gameObject) ? HandleConditionResultIsTrueForAction(isInRange, source) :
                                                            HandleConditionResultIsFalseForAction(isInRange, source);
            }
            else if(condition is HasEnoughResources)
            {
                HasEnoughResources hasEnough = condition as HasEnoughResources;
                return hasEnough.Check(source.BattlerData.Character) ? HandleConditionResultIsTrueForAction(hasEnough, source) :
                                                                       HandleConditionResultIsFalseForAction(hasEnough, source);        
            }
            else
            {
                return 0;
            }
        }

        private int HandleConditionResultIsTrueForAction(BehaviourCondition condition, BattlerBehaviour source)
        {
            if (condition.ActionWhenTrue.Abilities.Count > 0)
                return condition.ActionWhenTrue.Abilities[Rng.Next(condition.ActionWhenTrue.Abilities.Count)];
            else
                return ParseConditionOtherThanDefault(condition.ActionWhenTrue.Condition, source);
        }

        private int HandleConditionResultIsFalseForAction(BehaviourCondition condition, BattlerBehaviour source)
        {
            if (condition.ActionWhenFalse.Abilities.Count > 0)
                return condition.ActionWhenFalse.Abilities[Rng.Next(condition.ActionWhenFalse.Abilities.Count)];
            else
                return ParseConditionOtherThanDefault(condition.ActionWhenFalse.Condition, source);
        }
    }
}
