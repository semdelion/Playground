using Foundation;
using Semdelion.iOS;
using UIKit;

namespace Playground.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : BaseAppDelegate
    {
        public override BaseIosSetup MvxIosSetup()
        {
            return new Setup();
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Google.Maps.MapServices.ProvideApiKey("");//TODO add ApiKey
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}


