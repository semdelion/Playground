using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public override string Title => "Settings";

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
