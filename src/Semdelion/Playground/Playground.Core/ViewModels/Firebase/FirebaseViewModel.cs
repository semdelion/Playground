using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Firebase
{
    public class FirebaseViewModel : BaseViewModel
    {
        public override string Title => "Firebase";

        public FirebaseViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService) : base(loggerFactory, navigationService)
        {
        }
    }
}
