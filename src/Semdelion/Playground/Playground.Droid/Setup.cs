using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using Playground.Core;

namespace Playground.Droid
{
    public class Setup : MvxAndroidSetup <App>
    {

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(this.AndroidViewAssemblies);
        }
    }
}