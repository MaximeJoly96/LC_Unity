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
        private Language _currentLanguage;

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
        public Localization[] Localizations { get { return _localizations; } }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void LoadLanguage(Language language)
        {
            Localization localization = _localizations.FirstOrDefault(l => l.language == language);
            LoadLanguage(language, localization.languageFiles);
        }

        public void LoadLanguage(Language language, TextAsset[] files)
        {
            _currentLanguage = language;

            if(files != null)
            {
                ParseLanguageFiles(files);
                LanguageUpdated.Invoke();
            }
            else
            {
                LogsHandler.Instance.LogError("Cannot load language " + language + " because it is not supported by Localizer.");
            }
        }

        private void ParseLanguageFiles(TextAsset[] languageFiles)
        {
            _keyValuePairs = new Dictionary<string, string>();

            for(int l = 0; l < languageFiles.Length; l++)
            {
                string content = languageFiles[l].text;
                string[] lines = content.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitLine = lines[i].Split(';');

                    if (splitLine.Length == 2)
                        _keyValuePairs.Add(splitLine[0].Trim(), splitLine[1].Trim());
                }
            }
        }

        public string GetString(string key)
        {
            if (_keyValuePairs == null)
                LoadLanguage(_currentLanguage);

            return _keyValuePairs[key];
        }
    }
}
