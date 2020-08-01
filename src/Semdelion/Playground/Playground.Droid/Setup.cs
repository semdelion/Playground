using Android.Views;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using Playground.Core;
using Semdelion.Droid.Bindings;

namespace Playground.Droid
{
    public class Setup : MvxAndroidSetup <App>
    {

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(this.AndroidViewAssemblies);
        }

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			MvxAppCompatSetupHelper.FillTargetFactories(registry);
			base.FillTargetFactories(registry);
			registry.RegisterCustomBindingFactory<ViewGroup>(StatesTargetBinding.Key,
				viewGroup => new StatesTargetBinding(viewGroup));
		}
	}
}