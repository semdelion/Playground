using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Playground
{
    public class PlaygroundViewModel : BaseViewModel
    {
        public override string Title => "BaseViewModel";

        public PlaygroundViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService) : base(loggerFactory, navigationService)
        {

        }
    }
}
