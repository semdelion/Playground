using Android.OS;
using Android.Views;
using Android.Runtime;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Semdelion.Droid.Resources.View;

namespace Playground.Droid.View
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.main_layoutContent, false)]
    [Register(nameof(FirstView))]
    public class FirstView : MvxBaseFragment<FirstViewModel>
    {
        protected override int FragmentId => Resource.Layout.first_view;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            return view;
        }
    }
}