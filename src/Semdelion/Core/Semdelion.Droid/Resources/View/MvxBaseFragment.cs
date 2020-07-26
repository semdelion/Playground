using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Semdelion.Droid.Resources.View
{
    public abstract class MvxBaseFragment : MvxFragment
    {
        protected Toolbar _toolbar;

        protected abstract int FragmentId { get; }

        public MvxAppCompatActivity ParentActivity => (MvxAppCompatActivity)Activity;

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, null);
            return view;
        }
    }
    public abstract class MvxBaseFragment<TViewModel> : MvxBaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}