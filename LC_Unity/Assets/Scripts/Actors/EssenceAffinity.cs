using Essence;
using Core.Model;

namespace Actors
{
    public class EssenceAffinity
    {
        private ElementIdentifier _identifier;
        
        public int Id { get { return _identifier.Id; } }
        public string Name { get { return _identifier.NameKey; } }
        public string Description { get { return _identifier.DescriptionKey; } }
        public EssenceType Essence { get; private set; }
        public EssentialAffinityEffect Effect { get; private set; }

        public EssenceAffinity(ElementIdentifier identifier, EssenceType essence, EssentialAffinityEffect effect)
        {
            _identifier = identifier;
            Essence = essence;
            Effect = effect;
        }
    }
}
