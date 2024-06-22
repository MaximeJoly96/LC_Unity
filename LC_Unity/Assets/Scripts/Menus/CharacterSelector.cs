using UnityEngine;
using Actors;
using System.Collections.Generic;

namespace Menus
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField]
        private CharacterPreview _characterPreviewPrefab;

        public void Feed(List<Character> characters)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                CharacterPreview preview = Instantiate(_characterPreviewPrefab, transform);
                preview.Feed(characters[i]);
            }
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}
