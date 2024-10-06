using System;

namespace Inventory
{
    public class ItemRecipeComponent
    {
        public int ItemId { get; private set; }
        public int Quantity { get; private set; }

        public ItemRecipeComponent(int itemId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Provided quantity (" + quantity + 
                                            ") for recipe component is 0 or negative. This is not allowed. ItemId = " + itemId);

            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
