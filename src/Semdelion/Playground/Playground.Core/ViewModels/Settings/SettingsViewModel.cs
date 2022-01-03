using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Semdelion.Core.Providers.Interfaces;
using Semdelion.Core.ViewModels.Base;
using System.Globalization;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public override string Title => "Settings";

        protected IMvxLocalizationProvider LocalizationProvider { get; set; }

        public string CurrentCulture => $"{new CultureInfo(Semdelion.Core.User.Settings.Locale).NativeName}";

        public MvxObservableCollection<CultureInfo> Cultures { get; set; }

        public SettingsViewModel(
            ILoggerFactory loggerFactory, 
            IMvxNavigationService navigationService,
            IMvxLocalizationProvider localizationProvider) 
            : base(loggerFactory, navigationService) 
        {
            LocalizationProvider = localizationProvider;
        }

        public IMvxCommand OpenLogsCommand => new MvxAsyncCommand(async () => await NavigationService.Navigate<LogsViewModel>());
        public IMvxCommand ChangeLocale => new MvxAsyncCommand(DoChangeLocale);

        public async Task DoChangeLocale()
        {
            Semdelion.Core.User.Settings.Locale = LocalizationProvider.CurrentCultureInfo.Name == "ru-RU" ? "en-US" : "ru-RU";
            await LocalizationProvider.ChangeLocale(new CultureInfo(Semdelion.Core.User.Settings.Locale));
            await RaiseAllPropertiesChanged();
        }
    }
}
