using NUnit.Framework;
using Inventory;
using Core.Model;

namespace Testing.Inventory
{
    public class ResourceTest : TestFoundation
    {
        [Test]
        public void ResourceUsabilityShouldBeNever()
        {
            Resource res = new Resource(new ElementIdentifier(2, "name", "desc"), 3, 4, ItemCategory.Resource);

            Assert.AreEqual(ItemUsability.Never, res.Usability);
        }

        [Test]
        public void ResourceCanBeCreated()
        {
            Resource res = new Resource(new ElementIdentifier(8, "key", "item"), 2, 5, ItemCategory.Resource);

            Assert.AreEqual(8, res.Id);
            Assert.AreEqual("key", res.Name);
            Assert.AreEqual("item", res.Description);
            Assert.AreEqual(2, res.Icon);
            Assert.AreEqual(5, res.Price);
            Assert.AreEqual(ItemCategory.Resource, res.Category);
        }
    }
}
