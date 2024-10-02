using Core.Model;
using NUnit.Framework;

namespace Testing.Core.Model
{
    public class ElementIdentifierTests
    {
        [Test]
        public void ElementIdentifierCanBeCreated()
        {
            ElementIdentifier identifier = new ElementIdentifier(0, "nameKey", "descriptionKey");

            Assert.AreEqual(0, identifier.Id);
            Assert.AreEqual("nameKey", identifier.NameKey);
            Assert.AreEqual("descriptionKey", identifier.DescriptionKey);
        }
    }
}
