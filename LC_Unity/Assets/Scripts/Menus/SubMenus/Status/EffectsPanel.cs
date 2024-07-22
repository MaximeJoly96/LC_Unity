using UnityEngine;
using Actors;
using Language;

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
                effect.Feed(null, Localizer.Instance.GetString(character.ActiveEffects[i].Effect.ToString().ToLower()));
            }
        }

        protected override void Clear()
        {
            foreach (Transform child in _wrapper)
                Destroy(child.gameObject);
        }
    }
}
