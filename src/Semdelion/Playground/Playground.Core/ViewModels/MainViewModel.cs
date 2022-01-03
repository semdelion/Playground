using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Playground.Core.ViewModels.Firebase;
using Playground.Core.ViewModels.Map;
using Playground.Core.ViewModels.Phonebook;
using Playground.Core.ViewModels.Playground;
using Playground.Core.ViewModels.Settings;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        #region Commands
        private IMvxCommand _toSecondView;
        public IMvxCommand ToSecondView => _toSecondView ??= new MvxAsyncCommand(NavigateSecondView);
        public IMvxCommand<string> BottomNavigationItemSelectedCommand { get; private set; }
        #endregion

        List<BaseViewModel> _tabs;
        public List<BaseViewModel> Tabs
        {
            get => _tabs;
            set => SetProperty(ref _tabs, value);
        }

        #region Properties
        public override string Title => this["Title"];
        #endregion

        #region Constructor

        public MainViewModel(ILoggerFactory loggerFactory, IMvxNavigationService navigationService)
           : base(loggerFactory, navigationService)
        {
            BottomNavigationItemSelectedCommand = new MvxCommand<string>(BottomNavigationItemSelected);

            var tabs = new List<BaseViewModel>
            {
                Mvx.IoCProvider.IoCConstruct<PlaygroundViewModel>(),
                Mvx.IoCProvider.IoCConstruct<MapViewModel>(),
                Mvx.IoCProvider.IoCConstruct<ContactsViewModel>(),
                Mvx.IoCProvider.IoCConstruct<SettingsViewModel>(),
                Mvx.IoCProvider.IoCConstruct<FirebaseViewModel>()
            };

            Tabs = tabs;
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

        protected void BottomNavigationItemSelected(string tabId)
        {
            foreach (var item in Tabs)
                if (tabId == item.Key)
                {
                    NavigationService.Navigate(item);
                    break;
                }
        }
        #endregion
    }
}
