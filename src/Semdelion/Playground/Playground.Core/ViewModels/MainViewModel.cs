using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public override string Title => "MainViewModel";

        private IMvxCommand _toSecondView;
        public IMvxCommand ToSecondView => _toSecondView ??= new MvxAsyncCommand(NavigateSecondView);

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService)
        {
        }
        private async Task NavigateSecondView()
        {
           await NavigationService.Navigate<SecondViewModel>();
        }
    }
}
