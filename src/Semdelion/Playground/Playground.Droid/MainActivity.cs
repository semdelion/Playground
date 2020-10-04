using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Playground.Core.ViewModels;
using Plugin.Permissions;
using Semdelion.Droid.Resources.View;
using System.Collections.Generic;

namespace Playground.Droid
{
    [MvxActivityPresentation]
    [Activity( LaunchMode = LaunchMode.SingleTask,
               WindowSoftInputMode = SoftInput.StateHidden, 
               MainLauncher = true)]
    class MainActivity : BaseFragmentHostActivity<MainFragmentHostViewModel>, IMvxAndroidSharedElements
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            if (SupportActionBar == null)
            {
                var toolbar = FindViewById<Toolbar>(Resource.Id.main_toolbar);
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

        public IDictionary<string, View> FetchSharedElementsToAnimate(MvxBasePresentationAttribute attribute, MvxViewModelRequest request)
        {
            IDictionary<string, View> sharedElements = new Dictionary<string, View>();

            KeyValuePair<string, View>? photo = CreateSharedElementPair(Resource.String.transition_contact_photo);
            if (photo != null)
                sharedElements.Add(photo.GetValueOrDefault());

            KeyValuePair<string, View>? phone = CreateSharedElementPair(Resource.String.transition_contact_phone);
            if (phone != null)
                sharedElements.Add(phone.GetValueOrDefault());

            KeyValuePair<string, View>? name = CreateSharedElementPair(Resource.String.transition_contact_full_name);
            if (name != null)
                sharedElements.Add(name.GetValueOrDefault());

            return sharedElements;
        }

        private KeyValuePair<string, View>? CreateSharedElementPair(int tagStringResourceId)
        {
            var controlTag = Resources.GetString(tagStringResourceId);
            View control = FindViewById(Android.Resource.Id.Content).FindViewWithTag(controlTag);
            if (control != null)
            {
                control.Tag = null;
                return new KeyValuePair<string, View>(controlTag, control);
            }

            return null;
        }
    }
}