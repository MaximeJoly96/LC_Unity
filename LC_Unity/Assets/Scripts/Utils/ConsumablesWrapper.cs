using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    [Serializable]
    public class ConsumableDisplay
    {
        public int Id;
        public Sprite Icon;
    }

    public class ConsumablesWrapper : IconsWrapper
    {
        [SerializeField]
        private List<ConsumableDisplay> _consumables;

        public Sprite GetSpriteForConsumable(int id)
        {
            return _consumables.FirstOrDefault(c => c.Id == id).Icon;
        }
    }
}
