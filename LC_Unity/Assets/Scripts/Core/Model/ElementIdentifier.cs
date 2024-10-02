namespace Core.Model
{
    /// <summary>
    /// This class is used to set global identification information to all model objects (abilities, items, etc.)
    /// </summary>
    public class ElementIdentifier
    {
        public int Id { get; private set; }
        public string NameKey { get; private set; }
        public string DescriptionKey { get; private set; }

        public ElementIdentifier(int id, string nameKey, string descriptionKey)
        {
            Id = id;
            NameKey = nameKey;
            DescriptionKey = descriptionKey;
        }
    }
}
