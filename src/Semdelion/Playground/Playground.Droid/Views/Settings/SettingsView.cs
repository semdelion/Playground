using Android.Animation;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Widget;
using Com.Airbnb.Lottie;
using Java.Lang.Annotation;
using MvvmCross;
using MvvmCross.Platforms.Android;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Settings;
using Semdelion.Droid.Views;
using System;

namespace Playground.Droid.Views.Settings
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(SettingsView))]
    public class SettingsView : BaseFragment<SettingsViewModel>
    {
        protected override int FragmentId => Resource.Layout.settings_view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            NightModeCell(view);
            return view;
        }

        private void NightModeCell(View view)
        {
            var nightModeLayout = view.FindViewById<ConstraintLayout>(Resource.Id.settings_nightmode_layout);

            var nightModeLottie = view.FindViewById<LottieAnimationView>(Resource.Id.settings_nightmode_lottie);

            nightModeLottie.Progress = Semdelion.Core.User.Settings.ModeNight ? 1 : 0.0f;

            nightModeLayout.Click += (o, e) => 
            {
                var nightModeLottie = ((View)o).FindViewById<LottieAnimationView>(Resource.Id.settings_nightmode_lottie);
                Semdelion.Core.User.Settings.ModeNight = !Semdelion.Core.User.Settings.ModeNight;
                nightModeLottie.Speed = Semdelion.Core.User.Settings.ModeNight ? 2 : -2;
                nightModeLottie.PlayAnimation();
                nightModeLottie.AddAnimatorListener(new ChangeThemeAnimationListener());
            };
        }

        class ChangeThemeAnimationListener : Java.Lang.Object, Animator.IAnimatorListener
        {
            public void OnAnimationCancel(Animator animation) { }

            public void OnAnimationEnd(Animator animation)
            {
                var activity = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
                activity.Recreate();
            }

            public void OnAnimationRepeat(Animator animation) { }

            public void OnAnimationStart(Animator animation) { }
        }
    }
}