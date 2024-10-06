using Effects;
using Inventory;
using System.Collections.Generic;

namespace Actors.Equipment
{
    public class CharacterEquipment
    {
        public EquipmentSlot LeftHand { get; set; }
        public EquipmentSlot RightHand { get; set; }
        public EquipmentSlot Head { get; set; }
        public EquipmentSlot Body { get; set; }
        public EquipmentSlot Accessory { get; set; }

        public void Init()
        {
            Head = new EquipmentSlot(EquipmentPosition.Helmet, -1);
            LeftHand = new EquipmentSlot(EquipmentPosition.LeftHand, -1);
            RightHand = new EquipmentSlot(EquipmentPosition.RightHand, -1);
            Body = new EquipmentSlot(EquipmentPosition.Body, -1);
            Accessory = new EquipmentSlot(EquipmentPosition.Accessory, -1);
        }

        public List<IEffect> GetAllItemEffects()
        {
            List<IEffect> effects = new List<IEffect>();

            effects.AddRange(GetEffectsFromEquipmentSlot(RightHand));
            effects.AddRange(GetEffectsFromEquipmentSlot(LeftHand));
            effects.AddRange(GetEffectsFromEquipmentSlot(Head));
            effects.AddRange(GetEffectsFromEquipmentSlot(Body));
            effects.AddRange(GetEffectsFromEquipmentSlot(Accessory));

            return effects;
        }

        private List<IEffect> GetEffectsFromEquipmentSlot(EquipmentSlot slot)
        {
            List<IEffect> effects = new List<IEffect>();

            BaseItem item = slot.GetItem();
            if (item != null)
            {
                foreach (IEffect e in item.Effects)
                    effects.Add(e);
            }

            return effects;
        }
    }
}
