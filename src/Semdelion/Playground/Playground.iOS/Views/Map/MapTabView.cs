using MvvmCross.Platforms.Ios.Presenters.Attributes;
using Playground.Core.ViewModels.Map;
using Semdelion.iOS.Views.Base;
using UIKit;

namespace Playground.iOS.Views.Map
{
    [MvxTabPresentation(TabName = "Map", TabIconName = "home", TabSelectedIconName = "selected", WrapInNavigationController = false)]
    public partial class MapTabView : BaseViewController<MapViewModel>
    {
        protected override void ConfigureViews()
        {
            base.ConfigureViews();
            View.BackgroundColor = UIColor.Cyan;
        }
    }
}

