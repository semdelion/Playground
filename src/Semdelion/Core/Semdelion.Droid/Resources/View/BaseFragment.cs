using Android.Bluetooth;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Semdelion.Core.Enums;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Droid.Bindings;

namespace Semdelion.Droid.Resources.View
{
    public abstract class BaseFragment<TViewModel> : MvxFragment<TViewModel>
        where TViewModel : class, IBaseViewModel
    {

        protected Toolbar _toolbar;
        protected abstract int FragmentId { get; }

        /// <summary>
        /// 	Заголовок.
        /// </summary>
        private string _title;
        public virtual string Title
        {
            get => _title ??= ViewModel?.Title;

            set
            {
                _title = value;
                var mainActivity = Activity as AppCompatActivity;
                if (mainActivity?.SupportActionBar != null)
                {
                    mainActivity.SupportActionBar.Title = value;
                }
            }
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, container, false);
            Binding(view, container);
            return view;
        }

        protected virtual void Binding(Android.Views.View view, ViewGroup viewGroup)
        {
            var set = this.CreateBindingSet<BaseFragment<TViewModel>, TViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.Title);
            set.Apply();
        }
    }
}