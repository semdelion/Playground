namespace Semdelion.Droid
{
    using Android.Views;
    using Google.Android.Material.BottomNavigation;
    using Microsoft.Extensions.Logging;
    using MvvmCross;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Binding.Parse.Binding.Lang;
    using MvvmCross.Converters;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.Platforms.Android.Core;
    using MvvmCross.Platforms.Android.Presenters;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using Semdelion.Core;
    using Semdelion.Core.ViewModels.Base;
    using Semdelion.DAL.Helpers;
    using Semdelion.DAL.Helpers.Interfaces;
    using Semdelion.DAL.Services;
    using Semdelion.DAL.Services.Handlers;
    using Semdelion.Droid.Bindings;
    using Semdelion.Droid.Log;
    using System.Globalization;
    using System.Reflection;
    using Xamarin.Android.Net;
    using Serilog.Extensions.Logging;
    using Serilog;

    public abstract class BaseDroidSetup : MvxAndroidSetup
    {
        private App _app;

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAndroidViewPresenter(AndroidViewAssemblies);
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<ViewGroup>(
                StatesTargetBinding.Key, viewGroup => new StatesTargetBinding(viewGroup));
            registry.RegisterCustomBindingFactory<BottomNavigationView>(
                MvxBottomNavigationBinding.Key, view => new MvxBottomNavigationBinding(view));
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvxLanguageConverter());
        }

        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeFirstChance(iocProvider);
            Mvx.IoCProvider.RegisterSingleton<IConnectionService>(() => new ConnectionService(() => new HttpLoggingHandler(new AndroidClientHandler())));
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
            _app.InitializeCultureInfo(new CultureInfo(Core.User.Settings.Locale));
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
			   .WriteTo.AndroidLog()
			   .CreateLogger();

			return new SerilogLoggerFactory();
#endif
        }

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            Mvx.IoCProvider.CallbackWhenRegistered<IMvxLanguageBindingParser>(parser =>
            {
                var parserIntance = (MvxLanguageBindingParser)parser;
                parserIntance.DefaultTextSourceName = nameof(BaseViewModel.LocalizedTextSource);
                parserIntance.DefaultBindingMode = MvxBindingMode.OneWay;
            });
        }
    }
}