using Save.Model;
using NUnit.Framework;
using UnityEngine;

namespace Testing.Save.Model
{
    public class SaveDescriptorTests : TestFoundation
    {
        [Test]
        public void DescriptorCanBeCreated()
        {
            SaveDescriptor descriptor = new SaveDescriptor(5, 150, 32.5f);

            Assert.AreEqual(5, descriptor.Id);
            Assert.AreEqual(150, descriptor.MapId);
            Assert.IsTrue(Mathf.Abs(descriptor.InGameTime - 32.5f) < 0.01f);
        }
    }
}
