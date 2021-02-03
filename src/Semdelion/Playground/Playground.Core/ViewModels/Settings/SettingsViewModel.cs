using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.Log;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public override string Title => "Settings";

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ILogReader logReader, ILogRepository logRepository) : base(logProvider, navigationService)
        {

            var log =  logReader.ReadAll();
            foreach (var l in log)
            {
                LogText += l + "\n";
            }
        }

        public string LogText { get; set; }
    }
}
