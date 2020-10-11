using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Map
{
    public class MapViewModel : BaseViewModel
    {
        public override string Title => "Map";

        public MapViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
