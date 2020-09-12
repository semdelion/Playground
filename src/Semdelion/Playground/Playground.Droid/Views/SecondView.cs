using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Playground.Core.ViewModels;
using Semdelion.Droid.Views;

namespace Playground.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainFragmentHostViewModel), Resource.Id.main_layoutContent, true)]
    [Register(nameof(SecondView))]
    public class SecondView : BaseFragment<SecondViewModel>
    {
        protected override int FragmentId => Resource.Layout.second_view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            return view;
        }
    }
}