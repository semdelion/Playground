using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels
{
    public class MainFragmentHostViewModel : BaseViewModel
    {
        private IMvxCommand _firstViewModel;
        public IMvxCommand FirstViewModel => _firstViewModel ??= new MvxAsyncCommand(() => NavigationService.Navigate<MainViewModel>());

        public MainFragmentHostViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
