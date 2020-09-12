using MvvmCross.ViewModels;
using Semdelion.Core.ViewModels.Interfaces;
using Semdelion.Core.Enums;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace Semdelion.Core.ViewModels.Base
{
    public abstract class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>, IBaseViewModel<TParameter, TResult>
    {
        public virtual string Title => string.Empty;

        public States State { get; set; }

        public IMvxCommand RefreshCommand { get; set; } = null;

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {

        }
    }
}
