namespace Semdelion.iOS
{
    using MvvmCross.Platforms.Ios.Core;
    using MvvmCross.Platforms.Ios.Views;

    public abstract class BaseAppDelegate : MvxApplicationDelegate, IMvxCanCreateIosView
    {
        public abstract BaseIosSetup MvxIosSetup();
    }
}
