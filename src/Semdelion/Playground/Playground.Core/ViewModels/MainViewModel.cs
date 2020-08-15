using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;
using MvvmCross.Localization;

namespace Playground.Core.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        #region Commands

        private IMvxCommand _toSecondView;
        public IMvxCommand ToSecondView => _toSecondView ??= new MvxAsyncCommand(NavigateSecondView);

        #endregion

        #region Properties

        public override string Title => this["Title"];

        #endregion

        #region Constructor

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService)
        {
        }

        #endregion

        #region Private

        private async Task NavigateSecondView()
        {
            await NavigationService.Navigate<SecondViewModel>();
        }

        #endregion
    }
}
