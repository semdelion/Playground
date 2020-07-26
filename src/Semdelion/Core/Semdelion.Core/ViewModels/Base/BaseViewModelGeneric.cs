using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Core.Enums;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>, IBaseViewModel<TParameter, TResult>
    {
        public States State { get; set; } = States.Clean;

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
