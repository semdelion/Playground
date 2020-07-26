using MvvmCross.Platforms.Ios.Views;
using Semdelion.Core.ViewModels;

namespace Playground.iOS.Views
{
    public partial class RootView : MvxViewController<MainViewModel>
    {
        public RootView() : base("RootView", null)
        {
        }
    }
}

