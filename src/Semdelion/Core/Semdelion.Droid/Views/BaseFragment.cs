using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using Semdelion.Core.ViewModels.Interfaces;

namespace Semdelion.Droid.Views
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, container, false);
            Binding(view, container);
            return view;
        }

        protected virtual void Binding(View view, ViewGroup viewGroup)
        {
            var set = this.CreateBindingSet<BaseFragment<TViewModel>, TViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.Title);
            set.Apply();
        }
    }
}