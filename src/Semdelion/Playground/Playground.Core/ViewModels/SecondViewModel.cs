using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.Enums;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
           : base(logProvider, navigationService)
        {

        }

        public override async Task Initialize()
        {
            await base.Initialize();

            State = States.Error;
            await Task.Delay(3000);
            State = States.NoInternet;
            await Task.Delay(3000);
            State = States.NoData;
            await Task.Delay(3000);
            State = States.Loading;
            await Task.Delay(3000);
            State = States.Normal;
        }
    }
}
