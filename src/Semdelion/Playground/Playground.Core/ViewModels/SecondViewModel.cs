using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Playground.Core.Providers;
using Refit;
using Semdelion.Core.Enums;
using Semdelion.Core.Extensions;
using Semdelion.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace Playground.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        protected IContactProvider ContactProvider { get; }
        public override string Title => "SecondViewModel";

        public SecondViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IContactProvider contactProvider)
           : base(logProvider, navigationService)
        {
            ContactProvider = contactProvider;
        }


        private MvxAsyncCommand _refreshCommand;
        public override IMvxCommand RefreshCommand => _refreshCommand ??= new MvxAsyncCommand(async () => await Getdate());

        public async Task Getdate()
        {
            State = States.Loading;
            var requestResult = await ContactProvider.GetContacts(30, 1);

            this.UpdateState(requestResult);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            await Getdate();
        }
    }
}
