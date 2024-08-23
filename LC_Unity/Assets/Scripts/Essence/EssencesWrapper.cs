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
            EssentialAffinities = EssentialAffinitiesParser.GetEssentialAffinities(_essentialAffinities);
        }
    }
}
