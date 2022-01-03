namespace Semdelion.iOS
{
    using System.Globalization;
    using System.Net.Http;
    using System.Reflection;
    using Microsoft.Extensions.Logging;
    using MvvmCross;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Converters;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using Semdelion.Core;
    using Semdelion.Core.User;
    using Semdelion.DAL.Helpers;
    using Semdelion.DAL.Helpers.Interfaces;
    using Semdelion.DAL.Services;
    using Semdelion.DAL.Services.Handlers;
    using Semdelion.iOS.Bindings;
    using Semdelion.iOS.Custom;
    using Semdelion.iOS.Log;
    using Serilog;
    using Serilog.Extensions.Logging;

    public abstract  class BaseIosSetup : MvxIosSetup
    {
        private App _app;

        private Microsoft.Extensions.Logging.ILogger logger;
        protected Microsoft.Extensions.Logging.ILogger Logger => logger ??= Mvx.IoCProvider.Resolve<ILoggerFactory>().CreateLogger(nameof(BaseIosSetup));

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<EmptyDataSet>(StatesTargetBinding.Key, view => new StatesTargetBinding(view));
            base.FillTargetFactories(registry);
        }

        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            Mvx.IoCProvider.RegisterSingleton<IConnectionService>(() => new ConnectionService(() => new HttpLoggingHandler(new NSUrlSessionHandler())));
            base.InitializeFirstChance(iocProvider);
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvxLanguageConverter());
        }

        protected sealed override IMvxApplication CreateApp(IMvxIoCProvider iocProvider)
        {
            _app = CreateSemApp();
            return _app;
        }

        protected abstract App CreateSemApp();

        protected virtual IConfiguratorSettings CreateConfiguratorSettings(Assembly clientCoreAssembly)
        {
            return new ConfiguratorSettings()
            {
                CoreAssembly = clientCoreAssembly,
                Folder = "Configs",
                Parser = new ConfiguratorParser()
            };
        }

        protected override ILoggerProvider CreateLogProvider()
        {
#if !RELEASE
            Mvx.IoCProvider.RegisterType<ILoggerProvider, LogProvider>();
            return new LogProvider();
#else
			return new SerilogLoggerProvider();
#endif
        }

        protected override ILoggerFactory CreateLogFactory()
        {
#if !RELEASE
            var loggerProviders = new LoggerProviderCollection();
            loggerProviders.AddProvider(Mvx.IoCProvider.Resolve<ILoggerProvider>());
            Serilog.Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Providers(loggerProviders)
               .CreateLogger();

            var loggerFactory = new SerilogLoggerFactory();
            loggerFactory.AddProvider(Mvx.IoCProvider.Resolve<ILoggerProvider>());
            return loggerFactory;
#else
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NSLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
#endif
        }

        protected override void InitializeApp(IMvxPluginManager pluginManager, IMvxApplication app)
        {
            base.InitializeApp(pluginManager, app);

            if (!(app is App _app))
                return;

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => CreateConfiguratorSettings(_app.GetType().Assembly));
        }

        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
            _app.InitializeCultureInfo(new CultureInfo(Settings.Locale));
        }
    }
}
