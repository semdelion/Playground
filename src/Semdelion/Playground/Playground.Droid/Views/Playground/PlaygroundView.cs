using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Semdelion.Droid.Views;
using Playground.Core.ViewModels.Playground;

namespace Playground.Droid.Views.Playground
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, false)]
    [Register(nameof(PlaygroundView))]
    public class PlaygroundView : BaseFragment<PlaygroundViewModel>
    {
        protected override int FragmentId => Resource.Layout.playground_view;
    }
}
