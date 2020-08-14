﻿namespace Semdelion.iOS
{
    using System.Globalization;
    using MvvmCross.Binding.Bindings.Target.Construction;
    using MvvmCross.Converters;
    using MvvmCross.Localization;
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.ViewModels;
    using Semdelion.Core;
    using Semdelion.iOS.Bindings;
    using UIKit;

    public abstract  class BaseIosSetup : MvxIosSetup
    {
        private App _app;

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<UIView>(StatesTargetBinding.Key, view => new StatesTargetBinding(view));
            base.FillTargetFactories(registry);
        }
        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("Language", new MvxLanguageConverter());
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
            _app.InitializeCultureInfo(new CultureInfo("en-US"));
        }
    }
}
