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
        public virtual string Title { get; set; } = string.Empty;

        public States _state;
        public States State
        { 
            get => _state;
            set => SetProperty(ref _state, value);
        } 

        public BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService)
        {

        }
    }
}
