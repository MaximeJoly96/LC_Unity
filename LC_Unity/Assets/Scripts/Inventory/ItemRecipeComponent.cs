namespace Inventory
{
    public class ItemRecipeComponent
    {
        public int ItemId { get; private set; }
        public int Quantity { get; private set; }

        public ItemRecipeComponent(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
