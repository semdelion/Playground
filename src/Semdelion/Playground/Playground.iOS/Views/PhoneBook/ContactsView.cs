using System;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using Playground.iOS.Views.PhoneBook.Cells;
using Semdelion.iOS.Bindings;
using Semdelion.iOS.Custom;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.PhoneBook
{
    public partial class ContactsView : BaseViewController<SecondViewModel>
    {
        private MvxUIRefreshControl _refresh;

        public ContactsView() : base(nameof(ContactsView), null)
        {
        }

        protected override void Binding()
        {
            base.Binding();

            _refresh = new MvxUIRefreshControl();

            var emptyDataSet = new EmptyDataSet(ContentView, ViewModel.RefreshCommand);
            var source = new ContactsTableViewSource(TableView, ContactTableViewCell.Key, ContactTableViewCell.Key);

            var set = this.CreateBindingSet<ContactsView, SecondViewModel>();
            set.Bind(emptyDataSet).For(StatesTargetBinding.Key).To(vm => vm.State);
            set.Bind(source).To(vm => vm.Items);
            set.Bind(source).For(s => s.GetContactsCommand).To(vm => vm.LoadNextPage);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ItemClickCommand);
            set.Bind(_refresh).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(_refresh).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
            set.Apply();

            TableView.Source = source;
            TableView.RefreshControl = _refresh;
            TableView.ReloadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.DarkContent, animated);
        }

        public class ContactsTableViewSource : MvxSimpleTableViewSource
        {
            public ICommand GetContactsCommand { get; set; }

            public ContactsTableViewSource(IntPtr handle)
                : base(handle)
            {
            }

            public ContactsTableViewSource(UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null)
               : base(tableView, nibName, cellIdentifier, bundle)
            {
            }

            public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
            {
                if (indexPath.Row >= RowsInSection(tableView, indexPath.Section) - 2 && GetContactsCommand.CanExecute(null))
                    GetContactsCommand.Execute(null);
            }
        }
    }
}

