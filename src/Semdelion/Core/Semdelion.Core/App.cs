namespace Semdelion.Core
{
    using System.Globalization;
    using System.Reflection;
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.ViewModels;
    using Plugin.Connectivity;
    using Semdelion.Core.Providers;
    using Semdelion.Core.Providers.Interfaces;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Parser;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var assemblyConfig = new AssemblyContentConfig(GetType().GetTypeInfo().Assembly)
            {
                ResourceFolder = "Locales",
                ParserConfig = new ParserConfig
                {
                    ThrowWhenKeyNotFound = true
                }
            };

            I18N.Initialize(assemblyConfig);

            var textProvider = new MvxLocalizationProvider(assemblyConfig);

            Mvx.IoCProvider.RegisterSingleton<IMvxTextProvider>(textProvider);
            Mvx.IoCProvider.RegisterSingleton<IMvxLocalizationProvider>(textProvider);
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => CrossConnectivity.Current);

        }

        public void InitializeCultureInfo(CultureInfo cultureInfo)
        {
            var localizationProvider = Mvx.IoCProvider.Resolve<IMvxLocalizationProvider>();
            localizationProvider.ChangeLocale(cultureInfo).Wait();
        }
    }
}
