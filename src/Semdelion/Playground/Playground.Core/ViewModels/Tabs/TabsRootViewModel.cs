using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Playground.Core.ViewModels.Tabs
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
             _navigationService.Navigate(typeof(Tab1ViewModel)),
             _navigationService.Navigate(typeof(Tab2ViewModel))
            );
        }
    }
}
