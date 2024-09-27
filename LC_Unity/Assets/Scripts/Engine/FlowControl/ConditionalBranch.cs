namespace Engine.FlowControl
{
    public abstract class ConditionalBranch : BasicCondition
    {
        public string FirstMember { get; set; }
        public string SecondMember { get; set; }
    }
}
