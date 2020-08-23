namespace Semdelion.Droid
{
    using Android.Views;
    using MvvmCross;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Converters;
    using MvvmCross.Localization;
    using MvvmCross.Platforms.Android.Core;
    using MvvmCross.Platforms.Android.Presenters;
    using MvvmCross.ViewModels;
    using Semdelion.Core;
    using Semdelion.DAL.Services;
    using Semdelion.Droid.Bindings;
    using System.Globalization;
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
            registry.RegisterCustomBindingFactory<ViewGroup>(StatesTargetBinding.Key,
                viewGroup => new StatesTargetBinding(viewGroup));
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

        public override void InitializeSecondary()
        {
            base.InitializeSecondary();
            _app.InitializeCultureInfo(new CultureInfo("ru-RU"));
        }
    }
}