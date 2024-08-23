using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Utils
{
    [Serializable]
    public class WeaponDisplay
    {
        public int Id;
        public Sprite Icon;
    }

    public class WeaponsWrapper : MonoBehaviour
    {
        [SerializeField]
        private List<WeaponDisplay> _weapons;

        public Sprite GetSpriteForWeapon(int id)
        {
            WeaponDisplay weapon = _weapons.FirstOrDefault(w => w.Id == id);
            return weapon != null ? weapon.Icon : null;
        }
    }
}
