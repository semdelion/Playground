using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Firebase
{
    public class FirebaseViewModel : BaseViewModel
    {
        public override string Title => "Firebase";

        public FirebaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
