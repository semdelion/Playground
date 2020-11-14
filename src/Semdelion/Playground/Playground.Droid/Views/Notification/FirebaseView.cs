﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Firebase;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views.Notification
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(FirebaseView))]
    public class FirebaseView : BaseFragment<FirebaseViewModel>
    {
        protected override int FragmentId => Resource.Layout.playground_view;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}