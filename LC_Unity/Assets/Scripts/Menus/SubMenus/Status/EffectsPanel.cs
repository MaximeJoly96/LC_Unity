using UnityEngine;
using Actors;

namespace Menus.SubMenus.Status
{
    public class EffectsPanel : StatusSubPanel
    {
        [SerializeField]
        private EffectDisplay _effectDisplayPrefab;
        [SerializeField]
        private Transform _wrapper;

        public override void Feed(Character character)
        {
            Clear();

            for(int i = 0; i < character.ActiveEffects.Count; i++)
            {
                EffectDisplay effect = Instantiate(_effectDisplayPrefab, _wrapper);
                effect.Feed(null, character.ActiveEffects[i].Effect.ToString());
            }
        }

        protected override void Clear()
        {
            foreach (Transform child in _wrapper)
                Destroy(child.gameObject);
        }
    }
}
