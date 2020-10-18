using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Settings;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.Settings
{
    [MvxTabPresentation(TabName = "Settings", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class SettingsTabView : BaseViewController<SettingsViewModel>
    {
        protected override void ConfigureViews()
        {
            base.ConfigureViews();
        }
    }
}

