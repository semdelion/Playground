using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Services;
using Semdelion.Core.Enums;
using Semdelion.Core.ViewModels.Base;
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
            State = States.Normal;
            try
            {

               
                var t = await _contactService.GetContacts(10, 1);

                   var tt = t.Data;


                var tttt = tt;
            }
            catch (Exception ex)
            {
                var t = ex.Message;
            }
            //State = States.Error;
            //await Task.Delay(3000);
            //State = States.NoInternet;
            //await Task.Delay(3000);
            //State = States.NoData;
            //await Task.Delay(3000);
            //State = States.Loading;
            //await Task.Delay(3000);
            //State = States.Normal;
        }
    }
}
