using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Playground.Core.ViewModels.Firebase;
using Playground.Core.ViewModels.Map;
using Playground.Core.ViewModels.Phonebook;
using Playground.Core.ViewModels.Settings;

namespace Playground.Core.ViewModels
{
    public class TabsRootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public TabsRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Task SetupTabs()
        {
            return Task.WhenAll(
                _navigationService.Navigate(typeof(FirebaseViewModel)),
                _navigationService.Navigate(typeof(MapViewModel)),
                _navigationService.Navigate(typeof(ContactsViewModel)),
                _navigationService.Navigate(typeof(SettingsViewModel)));
        }
    }
}
