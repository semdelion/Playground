using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
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
            var button = view.FindViewById<Button>(Resource.Id.first_view_button_theme);
            if (button != null)
                button.Click += ChangeTheme;
            return view;
        }

        private void ChangeTheme(object o, EventArgs e)
        {
            if (AppCompatDelegate.DefaultNightMode == AppCompatDelegate.ModeNightYes)
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            else
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
        }
    }
}