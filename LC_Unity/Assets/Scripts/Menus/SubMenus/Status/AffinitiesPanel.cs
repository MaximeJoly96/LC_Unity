using UnityEngine;
using Utils;
using Actors;
using Language;

namespace Menus.SubMenus.Status
{
    public class AffinitiesPanel : StatusSubPanel
    {
        [SerializeField]
        private AffinityDisplay _affinityDisplayPrefab;
        [SerializeField]
        private Transform _wrapper;

        public override void Feed(Character character)
        {
            ElementsWrapper wrapper = FindObjectOfType<ElementsWrapper>();
            Clear();

            for(int i = 0; i < character.ElementalAffinities.Count; i++)
            {
                AffinityDisplay affinity = Instantiate(_affinityDisplayPrefab, _wrapper);
                Element element = character.ElementalAffinities[i].Element;
                affinity.Feed(wrapper.GetSpriteFromElement(element),
                              Localizer.Instance.GetString(element.ToString().ToLower()),
                              character.GetElementalAffinity(element).Multiplier);
            }
        }

        protected override void Clear()
        {
            foreach (Transform child in _wrapper)
                Destroy(child.gameObject);
        }
    }
}
