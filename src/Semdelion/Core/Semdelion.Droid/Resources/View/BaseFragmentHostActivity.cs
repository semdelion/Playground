using System;
using Android.OS;
using Android.Runtime;
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
            Xamarin.Essentials.Platform.Init(this, bundle);
            Binding();
        }

        protected virtual void Binding()
        {
            this.CreateBinding(this).For(v => v.Title).To<TViewModel>(vm => vm.Title).Apply();
        }
    }
}