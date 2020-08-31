using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels.Tabs;
using UIKit;

namespace Playground.iOS.Views.Tabs
{
    //[MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Tab 1")]
    [MvxTabPresentation(TabName = "Tab 1", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class Tab1View : MvxViewController<Tab1ViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.DarkGray;
            NavigationItem.Title = "Tab 1";
            var left = new UISwipeGestureRecognizer(() =>
            {
                var val1 = TabBarController.SelectedIndex;
               // var val2 = Convert.ToInt32(TabBarController.SelectedIndex + 1);
                TabBarController.SelectedIndex = 1;
            });
            left.Direction = UISwipeGestureRecognizerDirection.Left;
            this.View.AddGestureRecognizer(left);
        }
    }
}

