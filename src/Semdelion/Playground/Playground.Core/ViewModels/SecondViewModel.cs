using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService)
        {

        }
    }
}
