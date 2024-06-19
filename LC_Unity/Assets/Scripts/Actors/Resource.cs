namespace Actors
{
    public class Resource
    {
        public int MaxValue { get; set; }
        public int CurrentValue { get; set; }

        public Resource(int maxValue)
        {
            MaxValue = maxValue;
            CurrentValue = MaxValue;
        }
    }
}
