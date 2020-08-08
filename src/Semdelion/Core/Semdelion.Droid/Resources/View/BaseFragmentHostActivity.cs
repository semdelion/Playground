using System;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using Semdelion.Core.ViewModels.Interfaces;

namespace Semdelion.Droid.Resources.View
{
    public class BaseFragmentHostActivity<TViewModel> : MvxAppCompatActivity<TViewModel> where TViewModel : class, IBaseViewModel
    {
        public BaseFragmentHostActivity()
        {
        }

        protected BaseFragmentHostActivity(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

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