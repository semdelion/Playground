using MvvmCross.Logging;
using MvvmCross.Navigation;
using Semdelion.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playground.Core.ViewModels
{
    public class FirebaseViewModel : BaseViewModel
    {
        public FirebaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) 
            : base(logProvider, navigationService)
        {
        }
    }
}
