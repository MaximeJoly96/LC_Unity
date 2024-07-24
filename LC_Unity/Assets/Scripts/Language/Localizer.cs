using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Logging;
using UnityEngine.Events;

namespace Language
{
    public class Localizer : MonoBehaviour
    {
        [SerializeField]
        private Localization[] _localizations;

        private Dictionary<string, string> _keyValuePairs;

        private UnityEvent _languageUpdated;
        public UnityEvent LanguageUpdated
        {
            get
            {
                if (_languageUpdated == null)
                    _languageUpdated = new UnityEvent();

                return _languageUpdated;
            }
        }

        public static Localizer Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void LoadLanguage(Language language)
        {
            Localization localization = _localizations.FirstOrDefault(l => l.language == language);

            if(localization != null)
            {
                ParseLanguageFile(localization.languageFile);
                LanguageUpdated.Invoke();
            }
            else
            {
                LogsHandler.Instance.LogError("Cannot load language " + language + " because it is not supported by Localizer.");
            }
        }

        private void ParseLanguageFile(TextAsset languageFile)
        {
            string content = languageFile.text;
            string[] lines = content.Split('\n');

            _keyValuePairs = new Dictionary<string, string>();

            for(int i = 0; i < lines.Length; i++)
            {
                string[] splitLine = lines[i].Split(';');

                if(splitLine.Length == 2)
                    _keyValuePairs.Add(splitLine[0].Trim(), splitLine[1].Trim());
            }
        }

        public string GetString(string key)
        {
            return _keyValuePairs[key];
        }
    }
}
