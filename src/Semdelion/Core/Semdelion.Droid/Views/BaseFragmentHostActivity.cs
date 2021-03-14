using Android.OS;
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
            SetTheme(Core.User.Settings.ModeNight ? Resource.Style.AppThemeDark : Resource.Style.AppThemeLight);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Binding();
        }

        protected virtual void Binding()
        {
            this.CreateBinding(this).For(v => v.Title).To<TViewModel>(vm => vm.Title).Apply();
        }
    }
}