using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Playground
{
    public class PlaygroundViewModel : BaseViewModel
    {
        
        public override string Title => "BaseViewModel";

        public PlaygroundViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }
    }
}
