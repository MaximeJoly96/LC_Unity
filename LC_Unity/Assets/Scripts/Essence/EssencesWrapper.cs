using UnityEngine;
using System.Collections.Generic;
using Actors;

namespace Essence
{
    public class EssencesWrapper : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _essentialAffinities;

        public List<EssenceAffinity> EssentialAffinities { get; private set; }

        private void Awake()
        {
            FeedAffinities(_essentialAffinities);
        }

        public void FeedAffinities(TextAsset file)
        {
            if(file != null)
                EssentialAffinities = EssentialAffinitiesParser.GetEssentialAffinities(file);
        }
    }
}
