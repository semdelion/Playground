using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Services;
using Semdelion.Core.Enums;
using Semdelion.Core.ViewModels.Base;
using Semdelion.DAL.Exceptions;
using System;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        public override string Title => "SecondViewModel";
        public IContactService _contactService;

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IContactService contactService)
           : base(logProvider, navigationService)
        {
            _contactService = contactService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            try
            {
                State = States.Loading;
                var t = await _contactService.GetContacts(10, 1);
                var tt = t.Data;
                State = States.Normal;
            }
            catch (NetworkConnectionException ex)
            {
                State = States.NoInternet;
            }
            catch (Exception ex)
            {
                State = States.Error;
            }
            //State = States.NoData;
        }
    }
}
