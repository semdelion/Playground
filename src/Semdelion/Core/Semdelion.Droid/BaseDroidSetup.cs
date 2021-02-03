namespace Semdelion.Droid
{
    using Android.Views;
    using Google.Android.Material.BottomNavigation;
    using MvvmCross;
    using MvvmCross.Binding;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Binding.Parse.Binding.Lang;
    using MvvmCross.Converters;
    using MvvmCross.IoC;
    using MvvmCross.Localization;
    using MvvmCross.Logging;
    using MvvmCross.Platforms.Android.Core;
    using MvvmCross.Platforms.Android.Presenters;
    using MvvmCross.Plugin;
    using MvvmCross.ViewModels;
    using Semdelion.Core;
    using Semdelion.Core.ViewModels.Base;
    using Semdelion.DAL.Helpers;
    using Semdelion.DAL.Helpers.Interfaces;
    using Semdelion.DAL.Services;
    using Semdelion.Droid.Bindings;
    using Semdelion.Droid.Log;
    using System.Globalization;
    using System.Reflection;
    using Xamarin.Android.Net;

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

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.IoCProvider.RegisterSingleton<IConnectionService>(() => new ConnectionService(() => new AndroidClientHandler()));
        }

        protected sealed override IMvxApplication CreateApp()
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
            _app.InitializeCultureInfo(new CultureInfo("ru-RU"));
        }

        protected override IMvxLogProvider CreateLogProvider()
        {
#if !RELEASE
            Mvx.IoCProvider.RegisterType<IMvxLogProvider, LogProvider>();
            return new LogProvider();
#else
            return base.CreateLogProvider();
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