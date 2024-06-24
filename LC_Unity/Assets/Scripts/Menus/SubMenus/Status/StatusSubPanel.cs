using UnityEngine;
using Actors;

namespace Menus.SubMenus.Status
{
    public abstract class StatusSubPanel : MonoBehaviour
    {
        public abstract void Feed(Character character);
        protected virtual void Clear() { }
    }
}
