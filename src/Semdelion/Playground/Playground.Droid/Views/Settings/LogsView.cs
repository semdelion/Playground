using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Playground.Core.ViewModels.Settings;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views.Settings
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.Root_FrameLayout, true)]
    [Register(nameof(LogsView))]
    public class LogsView : BaseFragment<LogsViewModel> 
    {
        protected override int FragmentId => Resource.Layout.logs_view;
    }
}