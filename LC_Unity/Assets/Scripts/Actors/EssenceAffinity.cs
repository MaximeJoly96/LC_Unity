using Essence;

namespace Actors
{
    public class EssenceAffinity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EssenceType Essence { get; private set; }
        public EssentialAffinityEffect Effect { get; private set; }

        public EssenceAffinity(int id, string name, string description, EssenceType essence, EssentialAffinityEffect effect)
        {
            Id = id;
            Name = name;
            Description = description;
            Essence = essence;
            Effect = effect;
        }
    }
}
