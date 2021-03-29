namespace Semdelion.iOS
{
    using Foundation;
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.Platforms.Ios.Views;
    using UIKit;

    public abstract class BaseAppDelegate : MvxApplicationDelegate, IMvxCanCreateIosView
    {
        public abstract BaseIosSetup MvxIosSetup();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            base.FinishedLaunching(application, launchOptions);
            Window.OverrideUserInterfaceStyle = Core.User.Settings.ModeNight ? UIUserInterfaceStyle.Dark : UIUserInterfaceStyle.Light;
            return true;
        }
    }
}
