using System.Collections.Generic;

namespace Inventory
{
    public class ItemRecipe
    {
        public List<ItemRecipeComponent> Components { get; private set; }

        public ItemRecipe()
        {
            Components = new List<ItemRecipeComponent>();
        }

        public void AddComponent(ItemRecipeComponent component)
        {
            Components.Add(component);
        }
    }
}
