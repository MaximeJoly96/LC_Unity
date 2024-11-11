using System;
using UnityEngine;

namespace Language
{
    [Serializable]
    public class Localization
    {
        public Language language;
        public TextAsset[] languageFiles;
    }
}
