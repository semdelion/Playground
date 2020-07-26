using MvvmCross.Localization;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Semdelion.Core.Enums;
using Semdelion.Core.ViewModels.Interfaces;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel : MvxNavigationViewModel, IBaseViewModel
    {
        public virtual string Title => string.Empty;

        public States State { get; set; } = States.Clean;

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService)
        {

        }
    }
}
