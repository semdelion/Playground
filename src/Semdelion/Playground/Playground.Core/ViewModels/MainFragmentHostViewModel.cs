using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class MainFragmentHostViewModel : BaseViewModel
    {
        public MainFragmentHostViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            NavigationService.Navigate<MainViewModel>();
        }
    }
}
