using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Plugin.Permissions;
using Semdelion.Droid.Resources.View;

namespace Playground.Droid
{
    [MvxActivityPresentation]
    [Activity( LaunchMode = LaunchMode.SingleTask,
               WindowSoftInputMode = SoftInput.StateHidden, 
               MainLauncher = true)]
    class MainActivity : BaseFragmentHostActivity<MainFragmentHostViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            if (SupportActionBar == null)
            {
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);
                SetSupportActionBar(toolbar);
            }


            if (bundle == null)
                ViewModel.FirstViewModel.Execute(null);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}