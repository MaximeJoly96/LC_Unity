namespace Actors.Equipment
{
    public enum EquipmentPosition
    {
        Helmet,
        LeftHand,
        RightHand,
        Body,
        Accessory
    }

    public class EquipmentSlot
    {
        public EquipmentPosition Position { get; private set; }
        public int ItemId { get; set; }
        public string Name { get { return "NODATA"; } }
        
        public EquipmentSlot(EquipmentPosition position, int id)
        {
            Position = position;
            ItemId = id;
        }
    }
}
