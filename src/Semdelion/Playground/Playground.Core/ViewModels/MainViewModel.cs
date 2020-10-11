using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.ViewModels.Firebase;
using Playground.Core.ViewModels.Map;
using Playground.Core.ViewModels.Phonebook;
using Playground.Core.ViewModels.Settings;
using Semdelion.Core.ViewModels.Base;

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
            await NavigationService.Navigate<ContactsViewModel>();
        }

        #endregion

        #region Public

        public Task SetupTabs()
        {
            return Task.WhenAll(
                NavigationService.Navigate(typeof(FirebaseViewModel)),
                NavigationService.Navigate(typeof(MapViewModel)),
                NavigationService.Navigate(typeof(ContactsViewModel)),
                NavigationService.Navigate(typeof(SettingsViewModel)));
        }

        #endregion
    }
}
