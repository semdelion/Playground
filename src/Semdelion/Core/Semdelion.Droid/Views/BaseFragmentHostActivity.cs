using Android.OS;
using AndroidX.AppCompat.App;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using Semdelion.Core.ViewModels.Interfaces;

namespace Semdelion.Droid.Resources.View
{
    public class BaseFragmentHostActivity<TViewModel> : MvxActivity<TViewModel> where TViewModel : class, IBaseViewModel
    {
        public BaseFragmentHostActivity() : base() {}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetTheme(AppCompatDelegate.DefaultNightMode == AppCompatDelegate.ModeNightYes ?
                    Resource.Style.AppThemeDark : Resource.Style.AppThemeLight);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Binding();
        }

        protected virtual void Binding()
        {
            this.CreateBinding(this).For(v => v.Title).To<TViewModel>(vm => vm.Title).Apply();
        }
    }
}