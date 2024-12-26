using NUnit.Framework;
using Inventory;

namespace Testing.Inventory
{
    public class ItemRecipeTests : TestFoundation
    {
        [Test]
        public void BlankItemRecipeCanBeCreated()
        {
            ItemRecipe recipe = new ItemRecipe();

            Assert.AreEqual(0, recipe.Components.Count);
        }

        [Test]
        public void ComponentsCanBeAddedToItemRecipe()
        {
            ItemRecipe recipe = new ItemRecipe();
            ItemRecipeComponent component = new ItemRecipeComponent(5, 11);

            recipe.Components.Add(component);

            Assert.AreEqual(1, recipe.Components.Count);
            Assert.AreEqual(5, recipe.Components[0].ItemId);
            Assert.AreEqual(11, recipe.Components[0].Quantity);
        }
    }
}
