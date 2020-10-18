using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using UIKit;

namespace Playground.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class TabsRootView :  MvxTabBarViewController<MainViewModel>
    {
        private bool _tabsInitialized;

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                OverrideUserInterfaceStyle = UIUserInterfaceStyle.Dark;
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, animated);
                UINavigationBar.Appearance.TintColor = UIColor.White;
            }
            if (!_tabsInitialized)
            {
                await ViewModel.SetupTabs();
                _tabsInitialized = true;
            }
        }
    }
}

