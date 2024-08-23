using Language;
using Inventory;
using UnityEngine;

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
        public string Name 
        { 
            get 
            {
                BaseItem item = GameObject.FindObjectOfType<ItemsWrapper>().GetItemFromId(ItemId);
                return item == null ? "" : Localizer.Instance.GetString(item.Name);
            } 
        }
        
        public EquipmentSlot(EquipmentPosition position, int id)
        {
            Position = position;
            ItemId = id;
        }
    }
}
