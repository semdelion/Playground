using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;

namespace Playground.iOS.Views
{
    public partial class RootView : MvxViewController<RootViewModel>
    {
        public RootView() : base("RootView", null)
        {
        }
    }
}

