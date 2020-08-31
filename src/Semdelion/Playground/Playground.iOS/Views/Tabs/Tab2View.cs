using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels.Tabs;
using UIKit;

namespace Playground.iOS.Views.Tabs
{
    [MvxTabPresentation(TabName = "Tab 2", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class Tab2View : MvxViewController<Tab2ViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Cyan;
            NavigationItem.Title = "Tab 2";

            var left = new UISwipeGestureRecognizer(() =>
            {
                TabBarController.SelectedIndex = 0;
            });
            left.Direction = UISwipeGestureRecognizerDirection.Right;
            this.View.AddGestureRecognizer(left);
        }
    }
}

