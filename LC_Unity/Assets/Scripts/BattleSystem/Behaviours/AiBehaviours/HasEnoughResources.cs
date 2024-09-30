using Effects;

namespace BattleSystem.Behaviours.AiBehaviours
{
    public class HasEnoughResources : BehaviourCondition
    {
        public enum AmountType { Flat, FromAbility, Percentage }

        public AmountType Amount { get; private set; }
        public float Value { get; private set; }
        public Stat Resource { get; private set; } 

        public HasEnoughResources(AmountType amount, float value, Stat resource)
        {
            Amount = amount;
            Resource = resource;
            Value = value;
        }

        public override void Run()
        {

        }
    }
}
