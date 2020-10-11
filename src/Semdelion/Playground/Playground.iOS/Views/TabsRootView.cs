using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;

namespace Playground.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class TabsRootView :  MvxTabBarViewController<MainViewModel>
    {
        private bool _tabsInitialized;

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

