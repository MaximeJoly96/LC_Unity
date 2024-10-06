using NUnit.Framework;
using Inventory;
using System;

namespace Testing.Inventory
{
    public class ItemRecipeComponentTests
    {
        [Test]
        public void ComponentCanBeCreated()
        {
            ItemRecipeComponent component = new ItemRecipeComponent(4, 9);

            Assert.AreEqual(4, component.ItemId);
            Assert.AreEqual(9, component.Quantity);
            
            Assert.Throws<ArgumentException>(delegate { component =  new ItemRecipeComponent(3, -2); });
            Assert.Throws<ArgumentException>(delegate { component = new ItemRecipeComponent(8, 0); });
        }
    }
}
