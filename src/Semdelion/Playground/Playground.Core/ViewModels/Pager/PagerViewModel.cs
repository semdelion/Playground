using System;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;

namespace Playground.Core.ViewModels.Pager
{
    public class PagerViewModel: BaseViewModel
    {
        public PagerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}
