using MvvmCross.Platforms.Ios.Views;
using Playground.Core.ViewModels;

namespace Playground.iOS.Views
{
    public partial class SecondView : MvxViewController<SecondViewModel>
    {
        public SecondView() : base(nameof(SecondView), null)
        {
        }
    }
}

