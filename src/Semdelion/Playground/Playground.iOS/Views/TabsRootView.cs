using System;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;
using UIKit;

namespace Playground.iOS.Views
{
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class TabsRootView :  MvxTabBarViewController<TabsRootViewModel>
    {
        private bool _tabsInitialized;

      /*  public TabsRootView(NSCoder coder) : base(coder)
        {
        }

        public TabsRootView(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected internal TabsRootView(IntPtr handle) : base(handle)
        {
        }*/

      /*  public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationController.NavigationBar.ShadowImage = new UIImage();
            NavigationController.NavigationBar.Translucent = true;
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };
        }*/

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

