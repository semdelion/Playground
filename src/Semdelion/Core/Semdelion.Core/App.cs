namespace Semdelion.Core
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.ViewModels;
    using Plugin.Connectivity;
    using Semdelion.Core.Helpers.Interfaces;
    using Semdelion.Core.Helpers.Settings;
    using Semdelion.Core.Providers;
    using Semdelion.Core.Providers.Interfaces;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Parser;

    public class App : MvxApplication
    {
        public IEnvironment Environment { get; private set; }

        protected virtual IEnvironmentSettings CreateSettings() => new EnvironmentSettings(GetType().Assembly);

        public override void Initialize()
        {
            base.Initialize();

            Environment = new EnvironmentBuilder().SetSettings(CreateSettings()).Build();

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
            Mvx.IoCProvider.RegisterSingleton<IAppSettings>(new AppSettings(Environment));
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => CrossConnectivity.Current);

        }

        public void InitializeCultureInfo(CultureInfo cultureInfo)
        {
            var localizationProvider = Mvx.IoCProvider.Resolve<IMvxLocalizationProvider>();
            localizationProvider.ChangeLocale(cultureInfo).Wait();
        }
    }
}
