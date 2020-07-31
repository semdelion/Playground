using System;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Platforms.Ios.Core;
using Semdelion.iOS.Bindings;
using UIKit;

namespace Semdelion.iOS
{
    public abstract class BaseIosSetup : MvxIosSetup
    {
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<UIView>(StatesTargetBinding.Key, view => new StatesTargetBinding(view));
            base.FillTargetFactories(registry);
        }
    }
}
