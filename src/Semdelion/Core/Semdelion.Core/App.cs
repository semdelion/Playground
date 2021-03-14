namespace Semdelion.Core
{
    using MvvmCross;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.Logging;
    using MvvmCross.ViewModels;
    using Plugin.Connectivity;
    using Semdelion.Core.Log;
    using Semdelion.Core.Log.Repository;
    using Semdelion.Core.Providers;
    using Semdelion.Core.Providers.Interfaces;
    using Semdelion.DAL.Helpers.Interfaces;
    using Semdelion.DAL.Helpers.Settings;
    using System.Globalization;
    using System.Reflection;
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

            Mvx.IoCProvider.RegisterSingleton<IRepository>(() => new Repository());
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILogRepository, LogRepository>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILogWriter, LogWriter>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILogReader, LogReader>();

            var logger = Mvx.IoCProvider.Resolve<IMvxLogProvider>().GetLogFor(nameof(App));
            logger.Info("#################### Client Settings ####################");
            logger.Error("Error");
            logger.Fatal("Fatal");
            logger.Warn("Warn");
        }

        public void InitializeCultureInfo(CultureInfo cultureInfo)
        {
            var localizationProvider = Mvx.IoCProvider.Resolve<IMvxLocalizationProvider>();
            localizationProvider.ChangeLocale(cultureInfo).Wait();
        }
    }
}
