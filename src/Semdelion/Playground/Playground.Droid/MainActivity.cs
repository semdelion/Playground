﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Plugin.Permissions;
using Semdelion.Droid.Resources.View;

namespace Playground.Droid
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", 
        LaunchMode = LaunchMode.SingleTask, 
        ScreenOrientation = ScreenOrientation.Portrait,
        WindowSoftInputMode = SoftInput.StateHidden, 
        MainLauncher = true)]
    class MainActivity : BaseFragmentHostActivity<MainFragmentHostViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);
            if (this.SupportActionBar == null)
            {
                var toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.main_toolbar);
                this.SetSupportActionBar(toolbar);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}