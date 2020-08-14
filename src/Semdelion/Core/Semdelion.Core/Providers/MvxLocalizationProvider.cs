namespace Semdelion.Core.Providers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MvvmCross.Localization;
    using Semdelion.Core.Providers.Interfaces;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Configs;

    public class MvxLocalizationProvider : IMvxTextProvider, IMvxLocalizationProvider
    {
        private static II18N TextProvider => I18N.Instance;

        public MvxLocalizationProvider(AssemblyContentConfig config)
        {
            I18N.Initialize(config);
        }

        public MvxLocalizationProvider(RemoteContentConfig config)
        {
            I18N.Initialize(config);
        }

        public CultureInfo CurrentCultureInfo { get; private set; }

        public async Task ChangeLocale(CultureInfo cultureInfo)
        {
            await TextProvider.ChangeLocale(cultureInfo);
            CurrentCultureInfo = cultureInfo;
        }

        public IEnumerable<CultureInfo> GetAvailableCultures()
            => TextProvider.GetAvailableCultures();

        public string GetText(string key)
            => I18N.Instance.Translate(key, fallback: null);

        public string GetText(string namespaceKey, string typeKey, string name)
            => TextProvider.Translate(FindResolvedKey(namespaceKey, typeKey, name));

        public Task ChangeLocale(string locale) => TextProvider.ChangeLocale(locale);

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
            => TextProvider.Translate(FindResolvedKey(namespaceKey, typeKey, name), formatArgs);

        public bool TryGetText(out string textValue, string namespaceKey, string typeKey, string name)
        {
            textValue = GetText(namespaceKey, typeKey, name);
            return textValue != null;
        }

        public bool TryGetText(out string textValue, string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            if (!TryGetText(out textValue, namespaceKey, typeKey, name))
                return false;

            // Key is found but matching value is empty. Don't format but return true.
            if (string.IsNullOrEmpty(textValue))
                return true;

            textValue = string.Format(textValue, formatArgs);
            return true;
        }

        private static string FindResolvedKey(string namespaceKey, string typeKey, string name)
        {
            var resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
                resolvedKey = $"{typeKey}.{resolvedKey}";

            if (!string.IsNullOrEmpty(namespaceKey))
                resolvedKey = $"{namespaceKey}.{resolvedKey}";

            return resolvedKey;
        }
    }
}
