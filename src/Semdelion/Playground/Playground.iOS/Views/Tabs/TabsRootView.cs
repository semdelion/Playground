using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels.Tabs;

namespace Playground.iOS.Views.Tabs
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class TabsRootView :  MvxTabBarViewController<TabsRootViewModel>
    {
        private bool _tabsInitialized;
        public TabsRootView()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            /*let left = UISwipeGestureRecognizer(target: self, action: #selector(swipeLeft))
        left.direction = .left
        self.view.addGestureRecognizer(left)

        let right = UISwipeGestureRecognizer(target: self, action: #selector(swipeRight))
        right.direction = .right
        self.view.addGestureRecognizer(right)
            */
            var f = this;
           /* var left = new UISwipeGestureRecognizer(()=>
            {
                var val1 = TabBarController.ViewControllers.Count() - 1;
                var val2 = Convert.ToInt32(TabBarController.SelectedIndex + 1);
                TabBarController.SelectedIndex = Math.Min(val1, val2);
            });
            this.View.AddGestureRecognizer(left);
            var right = new UISwipeGestureRecognizer(() =>
            {
                var val2 = Convert.ToInt32(TabBarController.SelectedIndex -1);
                TabBarController.SelectedIndex = Math.Max(0, val2);
            });
            this.View.AddGestureRecognizer(right);*/
        }
        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (!_tabsInitialized)
            {
                await ViewModel.SetupTabs();
                _tabsInitialized = true;
            }
        }
    }
}

