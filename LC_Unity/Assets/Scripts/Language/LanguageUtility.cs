namespace Language
{
    public enum Language { English, French }

    public static class LanguageUtility
    {
        public static string TranslateLanguageLabel(Language language)
        {
            switch (language)
            {
                case Language.English:
                    return "English";
                case Language.French:
                    return "Français";
            }

            return "English";
        }
    }
}
