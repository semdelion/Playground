using Loymax.Module.ClientSettings.iOS.Custom.CollectionSource;
using MvvmCross.Binding.BindingContext;
using Playground.Core.ViewModels.Settings;
using Playground.iOS.Views.Settings.Cells;
using Playground.iOS.Views.Settings.Sources;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.Settings
{
    public partial class LogsView : BaseViewController<LogsViewModel>
    {
        protected UISearchController SearchController { get; set; }

        public LogsView() : base(nameof(LogsView), null)
        {
        }

        protected override void Binding()
        {
            base.Binding();
            var source = new LogsTableViewSource(TableView);

            var set = this.CreateBindingSet<LogsView, LogsViewModel>();
            set.Bind(source).For(v => v.ItemsSource).To(vm => vm.Items);
            set.Apply();

            TableView.Source = source;
            TableView.ReloadData();
        }

        protected override void ConfigureViews()
        {
            base.ConfigureViews();

            var flow = CollectionView.CollectionViewLayout as UICollectionViewFlowLayout;
            flow.EstimatedItemSize = UICollectionViewFlowLayout.AutomaticSize;

            CollectionView.AllowsMultipleSelection = true;
            CollectionView.BackgroundColor = UIColor.Clear;
            CollectionView.RegisterNibForCell(FiltersCollectionViewCell.Nib, FiltersCollectionViewCell.Key);
            CollectionView.Source = new FiltersCollectionSource(ViewModel);
            CollectionView.ReloadData();

            TableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 300;
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            TableView.AllowsSelection = false;

            CreateSearchController();
            ExtendedLayoutIncludesOpaqueBars = true;
            DefinesPresentationContext = true;
            EdgesForExtendedLayout = UIRectEdge.All;
            AttachSearchBar();
        }

        protected virtual void CreateSearchController()
        {
            SearchController = new UISearchController(searchResultsController: null)
            {
                HidesNavigationBarDuringPresentation = false,
                ObscuresBackgroundDuringPresentation = false
            };

            SearchController.SearchBar.TextChanged += (s, e) =>
            {
                ViewModel.SearchLine = ((UISearchBar)s).Text;
            };
            SearchController.SearchBar.CancelButtonClicked += (s, e) =>
            {
                ViewModel.SearchLine = ((UISearchBar)s).Text = string.Empty;
            };
        }

        protected void AttachSearchBar()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var field = SearchController.SearchBar.SearchTextField;
                field.BackgroundColor = UIColor.Clear;
                field.TextColor = UIColor.Black;
                field.TintColor = UIColor.White;

                var segment = UISegmentedControl.AppearanceWhenContainedIn(new[] { typeof(UISearchBar) });
                segment.BackgroundColor = UIColor.White;
                segment.SetTitleTextAttributes(new UITextAttributes
                {
                    TextColor = UIColor.DarkGray
                }, UIControlState.Normal);
                segment.SetTitleTextAttributes(new UITextAttributes
                {
                    TextColor = NavigationController.NavigationBar.BarTintColor
                }, UIControlState.Selected);

                var iconView = field.LeftView as UIImageView;
                iconView.Image = iconView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                iconView.TintColor = UIColor.Gray;
                NavigationItem.HidesSearchBarWhenScrolling = false;
                NavigationItem.SearchController = SearchController;
            }
        }
    }
}

