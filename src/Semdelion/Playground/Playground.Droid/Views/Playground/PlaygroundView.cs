using Android.OS;
using Android.Views;
using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Semdelion.Droid.Views;
using Android.Widget;
using System;
using AndroidX.AppCompat.App;
using Playground.Core.ViewModels.Playground;

namespace Playground.Droid.Views.Playground
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(PlaygroundView))]
    public class PlaygroundView : BaseFragment<PlaygroundViewModel>
    {
        protected override int FragmentId => Resource.Layout.playground_view;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var button = view.FindViewById<Button>(Resource.Id.first_view_button_theme);
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
