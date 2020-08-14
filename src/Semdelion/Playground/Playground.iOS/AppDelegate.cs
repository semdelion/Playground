using Foundation;
using Semdelion.iOS;

namespace Playground.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register(nameof(AppDelegate))]
    public class AppDelegate : BaseAppDelegate
    {
        public override BaseIosSetup MvxIosSetup()
        {
            return new Setup();
        }
    }
}


