using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using Playground.iOS.Custom.Cells;
using Semdelion.iOS.Bindings;
using Semdelion.iOS.Custom;
using Semdelion.iOS.Views.Base;

namespace Playground.iOS.Views
{
    public partial class SecondView : BaseViewController<SecondViewModel>
    {
        private MvxUIRefreshControl _refresh;

        public SecondView() : base(nameof(SecondView), null)
        {
        }

        protected override void Binding()
        {
            base.Binding();

            _refresh = new MvxUIRefreshControl();
            TableView.RefreshControl = _refresh;

            var emptyDataSet = new EmptyDataSet(ContentView, ViewModel.RefreshCommand);
            var source = new MvxSimpleTableViewSource(TableView, ContactTableViewCell.Key, ContactTableViewCell.Key);

            var set = this.CreateBindingSet<SecondView, SecondViewModel>();
            set.Bind(emptyDataSet).For(StatesTargetBinding.Key).To(vm => vm.State);
            set.Bind(source).To(vm => vm.Items);
            set.Bind(_refresh).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Bind(_refresh).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Apply();

            TableView.Source = source;
            TableView.ReloadData();
        }
    }
}

